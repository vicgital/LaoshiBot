using LaoshiBot.Application.Interfaces.Components;
using LaoshiBot.Application.Interfaces.Processors;
using LaoshiBot.Domain.Constants;
using LaoshiBot.Domain.Enums;
using LaoshiBot.Domain.Models;
using Microsoft.Extensions.Logging;

namespace LaoshiBot.Application.Processors
{
    public class WordProcessor
        (
            ILogger<WordProcessor> logger,
            ITranslatorComponent translatorComponent,
            ITextToSpeechComponent textToSpeechComponent,
            IStorageAccountComponent storageAccountComponent,
            IDalleImageGeneratorComponent dalleImageGeneratorComponent
        ) : IWordProcessor
    {
        private readonly ILogger<WordProcessor> _logger = logger;
        private readonly ITranslatorComponent _translatorComponent = translatorComponent;
        private readonly ITextToSpeechComponent _textToSpeechComponent = textToSpeechComponent;
        private readonly IStorageAccountComponent _storageAccountComponent = storageAccountComponent;
        private readonly IDalleImageGeneratorComponent _dalleImageGeneratorComponent = dalleImageGeneratorComponent;

        public async Task<ChineseFlashcard> ProcessChineseWordAsync(ChineseNotionInput notionInput)
        {
            ArgumentNullException.ThrowIfNull(notionInput);

            try
            {
                var id = Guid.NewGuid();
                var translation = notionInput.English;
                if (string.IsNullOrWhiteSpace(translation))
                {
                    translation = await _translatorComponent.TranslateText(notionInput.Chinese, TranslatorLanguageCodes.CHINESE_SIMPLIFIED, TranslatorLanguageCodes.ENGLISH);
                }

                var pinyin = await _translatorComponent.TransliterateText(notionInput.Chinese, TranslatorLanguageCodes.CHINESE_SIMPLIFIED, TransliterateScriptCodes.CHINESE_HAN);
                var audio = await _textToSpeechComponent.GetAudioSpeechFromText(notionInput.Chinese, TextToSpeechLanguageCodes.CHINESE_MANDARIN, TextToSpeechVoiceNames.GetRandomChineseVoiceName(), TextToSpeechProsodyRate.Slow);

                var audioUrl = string.Empty;
                if (audio is not null)
                {
                    audioUrl = await _storageAccountComponent.UploadAudioToStorageAccount(id, audio);
                }

                var imageUrl = string.Empty;
                if (translation is not null && notionInput.CreateImage)
                {
                    var image = await _dalleImageGeneratorComponent.GenerateImageFromChineseTextAsync(translation);
                    if (image is not null)
                    {
                        imageUrl = await _storageAccountComponent.UploadImageToStorageAccount(id, image);
                    }
                }

                ChineseFlashcard result = new(id, notionInput.Chinese, pinyin, translation ?? "", audioUrl, imageUrl, []);

                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Chinese word: {Chinese}", notionInput.Chinese);
                throw;
            }

        }
    }
}
