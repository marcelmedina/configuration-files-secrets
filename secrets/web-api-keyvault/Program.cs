using System.Text.Json;
using Azure.Identity;
using azure_function_keyvault.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SecretSettings>(builder.Configuration.GetSection("SecretSettings"));

if (!builder.Environment.IsDevelopment())
{
    var keyVaultName = builder.Configuration["KeyVaultName"] ?? throw new ArgumentNullException("KeyVaultName");
    var keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";

    builder.Configuration
        .AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
