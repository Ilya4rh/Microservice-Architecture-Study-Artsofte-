using ExampleCore.HttpLogic.Polly.Interfaces;
using Polly;

namespace ExampleCore.HttpLogic.Polly;

public class PollyService : IPollyService
{
    public async Task<HttpResponseMessage> ReplySendRequestAsync(HttpClient httpClient, HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        return await Policy
            .Handle<Exception>()
            .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(5))
            .ExecuteAsync(async () => await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken));
    }
}
