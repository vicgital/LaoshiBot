namespace LaoshiBot.Domain.Models
{
    public record ChineseFlashcard(Guid Id, string Chinese, string Pinyin, string English, string AudioUrl, string ImageUrl, List<string> Tags);

}
