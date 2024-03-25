namespace ExampleCore.HttpLogic.Polly.Interfaces;

public interface IPollyService
{
    Task<HttpResponseMessage> ReplySendRequestAsync(HttpClient httpClient, HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
}