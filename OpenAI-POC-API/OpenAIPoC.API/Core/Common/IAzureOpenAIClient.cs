using OpenAI.Chat;

namespace OpenAIPoC.API.Core.Common
{
    public interface IAzureOpenAIClient
    {
        ChatClient GetChatClient(string modelDeployment);
    }
}
