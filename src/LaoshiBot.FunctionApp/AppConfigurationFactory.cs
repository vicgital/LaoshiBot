using Microsoft.Extensions.Configuration;

namespace LaoshiBot.FunctionApp
{
    internal static class AppConfigurationFactory
    {
        public static IConfiguration GetAppConfigurationFromConnectionString()
        {
            // Replace with your Azure App Configuration connection string

            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return config;
        }
    }
}
