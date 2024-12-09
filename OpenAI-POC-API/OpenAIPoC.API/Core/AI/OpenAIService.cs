using Microsoft.Extensions.Options;
using OpenAI.Chat;
using OpenAIPoC.API.Core.AI.Exceptions;
using OpenAIPoC.API.Core.Common;
using OpenAIPoC.API.Options;
using System.ClientModel;

namespace OpenAIPoC.API.Core.AI
{
    public class OpenAIService : IOpenAIService
    {
        private readonly IAzureOpenAIClient _azureClient;
        private readonly string _modelDeployment;
        public OpenAIService(IAzureOpenAIClient azureClient, IOptions<AzureOpenAIOptions> options)
        {
            _azureClient = azureClient ?? throw new ArgumentNullException(nameof(azureClient));
            _modelDeployment = options.Value.ModelDeployment ?? throw new ArgumentNullException(nameof(options.Value.ModelDeployment));
        }

        public async Task<ChatCompletion> GetChatCompletion(ChatMessage[] messages)
        {
            try
            {
                ChatClient client = _azureClient.GetChatClient(_modelDeployment);
                ClientResult<ChatCompletion> result = await client.CompleteChatAsync(messages);

                return result.Value;
            }
            catch (Exception ex)
            {
                throw new OpenAIServiceException("There was a problem requesting chat completion", ex);
            }
        }
    }
}
