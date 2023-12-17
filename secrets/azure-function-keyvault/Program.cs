using azure_function_keyvault.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((ctx, config) =>
    {
        var currentDirectory = ctx.HostingEnvironment.ContentRootPath;

        if (ctx.HostingEnvironment.IsDevelopment())
        {
            config.SetBasePath(currentDirectory)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<SecretSettings>();
        }
        else
        {
            config.SetBasePath(currentDirectory)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            /*config.SetBasePath(currentDirectory)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                .AddAzureKeyVault(new Uri($"https://mykeyvault.vault.azure.net/"),
                    new DefaultAzureCredential());*/
        }

        config.Build();
    })
    .ConfigureServices((ctx, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.Configure<SecretSettings>(ctx.Configuration.GetSection("SecretSettings"));
    })
    .Build();

host.Run();
