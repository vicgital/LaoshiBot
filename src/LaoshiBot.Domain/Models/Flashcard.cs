namespace LaoshiBot.Domain.Models
{
    public record Flashcard(Guid Id, string Chinese, string Pinyin, string English, string AudioUrl, string ImageUrl, List<string> Tags);

}
