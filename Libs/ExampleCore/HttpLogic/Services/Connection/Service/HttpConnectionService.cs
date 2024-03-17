using ExampleCore.HttpLogic.Polly.Interfaces;
using ExampleCore.HttpLogic.Services.Connection.Data;
using ExampleCore.HttpLogic.Services.Interfaces;

namespace ExampleCore.HttpLogic.Services.Connection.Service;

public class HttpConnectionService : IHttpConnectionService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IPollyService pollyService;

    public HttpConnectionService(IHttpClientFactory httpClientFactory, IPollyService pollyService)
    {
        this.httpClientFactory = httpClientFactory;
        this.pollyService = pollyService;
    }

    public HttpClient CreateHttpClient(HttpConnectionData httpConnectionData)
    {
        var httpClient = string.IsNullOrWhiteSpace(httpConnectionData.ClientName)
            ? httpClientFactory.CreateClient()
            : httpClientFactory.CreateClient(httpConnectionData.ClientName);

        if (httpConnectionData.Timeout != null)
        {
            httpClient.Timeout = httpConnectionData.Timeout.Value;
        }

        return httpClient;
    }

    public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient,
        CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        var response = await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);

        return response;
    }

    public async Task<HttpResponseMessage> SendRequestPollyServiceAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient,
        CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        var response = await pollyService.ReplySendRequestAsync(httpClient, httpRequestMessage, cancellationToken, httpCompletionOption);

        return response;
    }
}