using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace OpenAIPoC.API.Infrastructure.CosmosDB
{
    public class KeyVaultConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _keyVaultUrl;
        private readonly string _secretName;

        public KeyVaultConnectionStringProvider(string keyVaultUrl, string secretName)
        {
            _keyVaultUrl = keyVaultUrl;
            _secretName = secretName;
        }

        public string GetConnectionString()
        {
            var client = new SecretClient(new Uri(_keyVaultUrl), new DefaultAzureCredential());
            KeyVaultSecret secret = client.GetSecret(_secretName);
            return secret.Value;
        }
    }
}