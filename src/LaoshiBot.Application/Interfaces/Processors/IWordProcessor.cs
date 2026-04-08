using LaoshiBot.Domain.Models;

namespace LaoshiBot.Application.Interfaces.Processors
{
    public interface IWordProcessor
    {
        Task<ChineseFlashcard> ProcessChineseWordAsync(ChineseNotionInput notionInput);
    }
}
