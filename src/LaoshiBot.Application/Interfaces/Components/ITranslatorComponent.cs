namespace LaoshiBot.Application.Interfaces.Components
{
    public interface ITranslatorComponent
    {
        Task<string> TranslateText(string text, string sourceLanguageCode, string targetLanguageCode);
        Task<string> TransliterateText(string text, string souceLanguageCode, string fromScriptCode);
    }
}
