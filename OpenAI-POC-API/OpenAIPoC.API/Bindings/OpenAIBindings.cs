using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using OpenAIPoC.API.Core.AI;
using OpenAIPoC.API.Core.Common;
using OpenAIPoC.API.Infrastructure.OpenAI;
using OpenAIPoC.API.Options;
using System.ClientModel;

public static class OpenAIBindings
{
    public static void Register(IServiceCollection services)
    {
        services.Configure<AzureOpenAIOptions>(services.BuildServiceProvider().GetService<IConfiguration>().GetSection(AzureOpenAIOptions.AzureOpenAI));
        services.AddScoped<IAzureOpenAIClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AzureOpenAIOptions>>().Value;
            var client = new AzureOpenAIClient(
                new Uri(options.ResourceUri),
                new ApiKeyCredential(options.ApiKey)
            );

            return new AzureOpenAIClientWrapper(client);
        });
        services.AddScoped<IOpenAIService, OpenAIService>();
    }
}
