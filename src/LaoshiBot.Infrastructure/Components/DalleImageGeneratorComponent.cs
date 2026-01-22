using LaoshiBot.Application.Interfaces.Components;
using Microsoft.Extensions.Logging;
using OpenAI.Images;

namespace LaoshiBot.Infrastructure.Components
{
    public class DalleImageGeneratorComponent(ImageClient client, ILogger<DalleImageGeneratorComponent> logger) : IDalleImageGeneratorComponent
    {
        private readonly ImageClient _client = client;
        private readonly ILogger<DalleImageGeneratorComponent> _logger = logger;

        private const string prompt = 
                "Create an illustrative and stylized educational image for a study flashcard using this phrase: \"{0}\". " +
                "Do NOT include any text in the image at all." +
                "I don't wan't any letters, just illustrative images";


        public async Task<Stream?> GenerateImageFromChineseTextAsync(string text)
        {
            try
            {
                var fullPrompt = string.Format(prompt, text);

                var response = await _client.GenerateImageAsync(fullPrompt, new ImageGenerationOptions
                {
                    EndUserId = "xuecard-api",
                    Size = GeneratedImageSize.W1024xH1024,
                    Quality = GeneratedImageQuality.Standard,
                    Style = GeneratedImageStyle.Vivid,
                    ResponseFormat = GeneratedImageFormat.Bytes
                });

                var streamResult = response.Value.ImageBytes.ToStream();
                return streamResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GenerateImageFromChineseTextAsync({text})", text);
                return null;
            }
        }
    }
}
