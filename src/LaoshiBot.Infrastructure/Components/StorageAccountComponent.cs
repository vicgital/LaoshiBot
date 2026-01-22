using LaoshiBot.Application.Interfaces.Components;
using LaoshiBot.Infrastructure.AzureClients;
using Microsoft.Extensions.Logging;

namespace LaoshiBot.Infrastructure.Components
{
    public class StorageAccountComponent(
        LaoshiBotBlobClients blobClients,
        ILogger<TextToSpeechComponent> logger) : IStorageAccountComponent
    {
        private readonly LaoshiBotBlobClients _blobClients = blobClients;
        private readonly ILogger<TextToSpeechComponent> _logger = logger;


        public async Task<string> UploadAudioToStorageAccount(Stream audio)
        {
            if (_blobClients.AudioBlobContainerClient == null)
                throw new Exception("AudioBlobContainerClient is null");

            var blobClient = _blobClients.AudioBlobContainerClient.GetBlobClient($"{Guid.NewGuid()}.wav");
            await blobClient.UploadAsync(audio);
            return blobClient.Uri.ToString();
        }

        public async Task<string> UploadImageToStorageAccount(Stream audio)
        {
            if (_blobClients.ImageBlobContainerClient == null)
                throw new Exception("ImageBlobContainerClient is null");

            var blobClient = _blobClients.ImageBlobContainerClient.GetBlobClient($"{Guid.NewGuid()}.png");
            await blobClient.UploadAsync(audio);
            return blobClient.Uri.ToString();
        }


    }
}
