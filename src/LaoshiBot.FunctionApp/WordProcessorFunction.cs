using LaoshiBot.Application.Interfaces.Processors;
using LaoshiBot.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LaoshiBot.FunctionApp
{
    public class WordProcessorFunction(ILogger<WordProcessorFunction> logger, IWordProcessor wordProcessor)
    {
        private readonly ILogger<WordProcessorFunction> _logger = logger;
        private readonly IWordProcessor _wordProcessor = wordProcessor;

        [Function("ProcessChineseWord")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                var input = await JsonSerializer.DeserializeAsync<ChineseNotionInput>(req.Body);
                if (input is null || string.IsNullOrEmpty(input.Chinese))
                {
                    return new BadRequestResult();
                }

                var result = await _wordProcessor.ProcessChineseWordAsync(input);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Chinese word.");
                throw;
            }

        }
    }
}
