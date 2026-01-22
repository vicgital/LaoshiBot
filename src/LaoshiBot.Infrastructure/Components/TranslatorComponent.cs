using Azure.AI.Translation.Text;
using LaoshiBot.Application.Interfaces.Components;
using Microsoft.Extensions.Logging;

namespace LaoshiBot.Infrastructure.Components
{
    public class TranslatorComponent(TextTranslationClient client, ILogger<TranslatorComponent> logger) : ITranslatorComponent
    {

        private readonly TextTranslationClient _client = client;
        private readonly ILogger<TranslatorComponent> _logger = logger;

        public async Task<string> TranslateText(string text, string sourceLanguageCode, string targetLanguageCode)
        {
            try
            {
                var options = new TextTranslationTranslateOptions(targetLanguageCode, text)
                {
                    SourceLanguage = sourceLanguageCode,
                    TextType = TextType.Plain,
                };

                var translationResult = await _client.TranslateAsync(options);

                var result = translationResult.Value;

                TranslatedTextItem? translatedText = result[0];
                if (translatedText is not null)
                    return translatedText.Translations[0].Text;
                else
                    throw new OperationCanceledException("Unable to translate Text");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TranslateText({text})", text);
                return string.Empty;
            }
        }

        public async Task<string> TransliterateText(string text, string souceLanguageCode, string fromScriptCode)
        {
            try
            {
                var options = new TextTranslationTransliterateOptions(souceLanguageCode, fromScriptCode, "Latn", text);

                var transliteratorResult = await _client.TransliterateAsync(options);
                var result = transliteratorResult.Value;

                var translatedText = result[0];
                if (translatedText is not null)
                    return translatedText.Text;
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TransliterateText()");
                return string.Empty;
            }
        }
    }
}
