using System.Net;
using azure_function_keyvault.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace azure_function_keyvault
{
    public class Secrets(
        IOptionsSnapshot<SecretSettings> settings,
        ILoggerFactory loggerFactory,
        IConfiguration configuration)
    {
        private readonly SecretSettings _settings = settings.Value;
        private readonly ILogger _logger = LoggerFactoryExtensions.CreateLogger<Secrets>(loggerFactory);

        [Function(nameof(GetSecrets))]
        public async Task<HttpResponseData> GetSecrets([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("Secrets retrieved.");
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_settings);
            return response;
        }

        [Function(nameof(GetProviders))]
        public IEnumerable<string?> GetProviders([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            var configRoot = (IConfigurationRoot)configuration;
            return configRoot.Providers.Select(p => p?.ToString());
        }
    }
}
