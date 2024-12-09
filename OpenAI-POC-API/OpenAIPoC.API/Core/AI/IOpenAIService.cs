using OpenAI.Chat;

namespace OpenAIPoC.API.Core.AI
{
    public interface IOpenAIService
    {
        public Task<ChatCompletion> GetChatCompletion(ChatMessage[] messages);
    }
}
