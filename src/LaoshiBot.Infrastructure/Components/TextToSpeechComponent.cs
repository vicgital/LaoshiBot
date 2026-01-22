using LaoshiBot.Application.Interfaces.Components;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Logging;

namespace LaoshiBot.Infrastructure.Components
{
    public class TextToSpeechComponent(SpeechConfig speechConfig, ILogger<TextToSpeechComponent> logger) : ITextToSpeechComponent
    {
        private readonly SpeechConfig _speechConfig = speechConfig;
        private readonly ILogger<TextToSpeechComponent> _logger = logger;

        public async Task<Stream?> GetAudioSpeechFromText(string text, string languageCode, string voiceName, string audioProsodyRate)
        {
            try
            {
                _speechConfig.SpeechSynthesisVoiceName = voiceName;
                _speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio48Khz192KBitRateMonoMp3);

                using var synthesizer = new SpeechSynthesizer(_speechConfig);

                string ssml = @$"
                            <speak version='1.0' xml:lang='{languageCode}'>
                              <voice name='{voiceName}'>
                                <prosody rate='{audioProsodyRate}' pitch='0%'>
                                  {text}
                                </prosody>
                              </voice>
                            </speak>";

                var speechSynthesisResult = await synthesizer.SpeakSsmlAsync(ssml);

                if (speechSynthesisResult.Reason == ResultReason.SynthesizingAudioCompleted)
                {
                    MemoryStream stream = new(speechSynthesisResult.AudioData)
                    {
                        Position = 0
                    };
                    return stream;
                }
                else
                    throw new Exception("Unable to Get Audio from Text");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAudioSpeechFromText({text}, {languageCode}, {voiceName}, {audioProsodyRate})", text, languageCode, voiceName, audioProsodyRate);
                return null;
            }
        }
    }
}
