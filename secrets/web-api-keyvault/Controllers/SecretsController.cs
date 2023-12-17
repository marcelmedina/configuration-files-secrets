using azure_function_keyvault.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace web_api_keyvault.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretsController : ControllerBase
    {
        private readonly SecretSettings _settings;
        private readonly ILogger<SecretsController> _logger;
        private readonly IConfiguration _configuration;

        public SecretsController(
            IOptions<SecretSettings> settings,
            ILogger<SecretsController> logger,
            IConfiguration configuration)
        {
            _settings = settings.Value;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public SecretSettings Get()
        {
            _logger.LogInformation("Secrets retrieved.");
            return _settings;
        }

        [HttpGet("Providers")]
        public IEnumerable<string?> GetProviders()
        {
            var configRoot = (IConfigurationRoot)_configuration;
            return configRoot.Providers.Select(p => p?.ToString());
        }
    }
}
