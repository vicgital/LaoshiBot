using Azure;
using Azure.AI.OpenAI;
using Azure.AI.Translation.Text;
using Azure.Storage.Blobs;
using LaoshiBot.Infrastructure.Constants;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;
using OpenAI.Images;
using System.ClientModel;

namespace LaoshiBot.Infrastructure.AzureClients
{
    public class AzureClientFactory
    {
        public static TextTranslationClient GetTextTranslationClient(IConfiguration config)
        {
            var key = config[AppConfigurationKeyNames.LAOSHIBOT_TRANSLATOR_SERVICES_KEY] ?? throw new ArgumentNullException($"{AppConfigurationKeyNames.LAOSHIBOT_TRANSLATOR_SERVICES_KEY} not found");
            var region = config[AppConfigurationKeyNames.LAOSHIBOT_TRANSLATOR_SERVICES_REGION] ?? throw new ArgumentNullException($"{AppConfigurationKeyNames.LAOSHIBOT_TRANSLATOR_SERVICES_REGION} not found"); ;
            AzureKeyCredential credential = new(key);
            TextTranslationClient client = new(credential, region);
            return client;
        }

        public static SpeechConfig GetSpeechSynthesizerConfig(IConfiguration config)
        {
            string aiSvcKey = config[AppConfigurationKeyNames.LAOSHIBOT_SPEECH_SERVICES_KEY] ?? throw new ArgumentNullException($"{AppConfigurationKeyNames.LAOSHIBOT_SPEECH_SERVICES_KEY} not found");
            string aiSvcRegion = config[AppConfigurationKeyNames.LAOSHIBOT_SPEECH_SERVICES_REGION] ?? throw new ArgumentNullException($"{AppConfigurationKeyNames.LAOSHIBOT_SPEECH_SERVICES_REGION} not found");

            // Configure speech service
            return SpeechConfig.FromSubscription(aiSvcKey, aiSvcRegion);
        }

        public static ImageClient GetOpenAIImageClient(IConfiguration config)
        {
            string openAiEndpoint = config[AppConfigurationKeyNames.LAOSHIBOT_OPENAI_ENDPOINT] ?? throw new ArgumentNullException($"{AppConfigurationKeyNames.LAOSHIBOT_OPENAI_ENDPOINT} not found");
            string openAiKey = config[AppConfigurationKeyNames.LAOSHIBOT_OPENAI_KEY] ?? throw new ArgumentNullException($"{AppConfigurationKeyNames.LAOSHIBOT_OPENAI_KEY} not found");
            string openAiRegion = config[AppConfigurationKeyNames.LAOSHIBOT_OPENAI_REGION] ?? throw new ArgumentNullException($"{AppConfigurationKeyNames.LAOSHIBOT_OPENAI_REGION} not found");
            string openAiModel = config[AppConfigurationKeyNames.LAOSHIBOT_OPENAI_DALLE_MODEL] ?? throw new ArgumentNullException($"{AppConfigurationKeyNames.LAOSHIBOT_OPENAI_DALLE_MODEL} not found");


            AzureOpenAIClient azureClient = new(new Uri(openAiEndpoint), new ApiKeyCredential(openAiKey));
            ImageClient client = azureClient.GetImageClient(openAiModel);
            return client;
        }

        public static LaoshiBotBlobClients GetStorageBlobContainerClients(IConfiguration config)
        {
            string storageAccountConnectionString = config[AppConfigurationKeyNames.LAOSHIBOT_STORAGE_CONNECTION_STRING] ?? throw new ArgumentNullException($"{AppConfigurationKeyNames.LAOSHIBOT_STORAGE_CONNECTION_STRING} not found");

            var blobServiceClient = new BlobServiceClient(storageAccountConnectionString);
            var audioBlobContainerClient = blobServiceClient.GetBlobContainerClient("audio");
            var imageBlobContainerClient = blobServiceClient.GetBlobContainerClient("image");

            LaoshiBotBlobClients clients = new(audioBlobContainerClient, imageBlobContainerClient);

            return clients;

        }
    }
}
