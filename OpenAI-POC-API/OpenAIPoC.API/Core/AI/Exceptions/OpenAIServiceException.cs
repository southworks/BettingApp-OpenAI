namespace OpenAIPoC.API.Core.AI.Exceptions
{
    public class OpenAIServiceException : Exception
    {
        public OpenAIServiceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
