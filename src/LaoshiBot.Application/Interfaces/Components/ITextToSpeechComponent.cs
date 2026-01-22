using System;
using System.Collections.Generic;
using System.Text;

namespace LaoshiBot.Application.Interfaces.Components
{
    public interface ITextToSpeechComponent
    {
        Task<Stream?> GetAudioSpeechFromText(string text, string languageCode, string voiceName, string audioProsodyRate);
    }
}
