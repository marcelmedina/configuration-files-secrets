namespace azure_function_keyvault.Models
{
    public class SecretSettings
    {
        public string SqlConnectionString { get; set; }
        public string BlobConnectionString { get; set; }
    }
}
