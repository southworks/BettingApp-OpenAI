namespace OpenAIPoC.API.Options
{
    public class AzureOpenAIOptions
    {
        public const string AzureOpenAI = "AzureOpenAI";
        public required string ResourceUri { get; set; }
        public required string ApiKey { get; set; }
        public required string ModelDeployment { get; set; }
    }
}
