using LaoshiBot.Domain.Enums;

namespace LaoshiBot.Application.Interfaces.Components
{
    public interface ITextToSpeechComponent
    {
        Task<Stream?> GetAudioSpeechFromText(string text, string languageCode, string voiceName, TextToSpeechProsodyRate audioProsodyRate);
    }
}
