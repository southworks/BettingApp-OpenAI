using Azure.AI.OpenAI;
using OpenAI.Chat;
using OpenAIPoC.API.Core.Common;

namespace OpenAIPoC.API.Infrastructure.OpenAI
{
    public class AzureOpenAIClientWrapper : IAzureOpenAIClient
    {
        private readonly AzureOpenAIClient _azureClient;

        public AzureOpenAIClientWrapper(AzureOpenAIClient azureClient)
        {
            _azureClient = azureClient ?? throw new ArgumentNullException(nameof(azureClient));
        }

        public ChatClient GetChatClient(string modelDeployment)
        {
            return _azureClient.GetChatClient(modelDeployment);
        }
    }
}
