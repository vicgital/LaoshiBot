using LaoshiBot.Application.Interfaces.Components;
using LaoshiBot.Application.Interfaces.Processors;
using LaoshiBot.Application.Processors;
using LaoshiBot.Infrastructure.AzureClients;
using LaoshiBot.Infrastructure.Components;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

var config = builder.Configuration; // use Functions host configuration

// Register Azure Clients
builder.Services
    .AddSingleton(AzureClientFactory.GetTextTranslationClient(config))
    .AddSingleton(AzureClientFactory.GetSpeechSynthesizerConfig(config))
    .AddSingleton(AzureClientFactory.GetOpenAIImageClient(config))
    .AddSingleton(AzureClientFactory.GetStorageBlobContainerClients(config));


builder.Services.AddLogging();
builder.Services.AddSingleton<ITextToSpeechComponent, TextToSpeechComponent>();
builder.Services.AddSingleton<ITranslatorComponent, TranslatorComponent>();
builder.Services.AddSingleton<IDalleImageGeneratorComponent, DalleImageGeneratorComponent>();
builder.Services.AddSingleton<IStorageAccountComponent, StorageAccountComponent>();

builder.Services.AddScoped<IWordProcessor, WordProcessor>();


builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
