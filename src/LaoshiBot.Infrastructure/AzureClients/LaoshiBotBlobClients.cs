using Azure.Storage.Blobs;

namespace LaoshiBot.Infrastructure.AzureClients
{
    public class LaoshiBotBlobClients(BlobContainerClient audioBlobContainerClient, BlobContainerClient imageBlobContainerClient)
    {
        public BlobContainerClient? AudioBlobContainerClient { get; } = audioBlobContainerClient;
        public BlobContainerClient? ImageBlobContainerClient { get; } = imageBlobContainerClient;
    }
}
