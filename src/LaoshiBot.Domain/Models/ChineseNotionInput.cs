using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaoshiBot.Domain.Models
{
    public class ChineseNotionInput
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Chinese input is required")]
        [JsonPropertyName("chinese")]
        public string Chinese { get; set; } = string.Empty;
        [JsonPropertyName("english")]
        public string English { get; set; } = string.Empty;
        [JsonPropertyName("createImage")]
        public bool CreateImage { get; set; }
    }
}
