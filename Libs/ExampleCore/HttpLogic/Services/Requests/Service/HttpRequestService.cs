using System.Net.Mime;
using System.Text;
using ExampleCore.HttpLogic.Services.Connection.Data;
using ExampleCore.HttpLogic.Services.Interfaces;
using ExampleCore.HttpLogic.Services.Requests.Data;
using ExampleCore.TraceLogic.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ContentType = ExampleCore.HttpLogic.Services.Data.ContentType;

namespace ExampleCore.HttpLogic.Services.Requests.Service;

internal class HttpRequestService(IHttpConnectionService httpConnectionService, IEnumerable<ITraceWriter> traceWriterList,
    IEnumerable<ITraceReader> traceReaders) : IHttpRequestService
{
    private readonly IHttpConnectionService httpConnectionService = httpConnectionService;
    private readonly IEnumerable<ITraceWriter> traceWriterList = traceWriterList;
    private readonly IEnumerable<ITraceReader> traceReaders = traceReaders;

    public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData)
    {
        var httpClient = httpConnectionService.CreateHttpClient(connectionData);
        var httpRequestMessage = new HttpRequestMessage(requestData.Method, requestData.Uri);

        foreach (var traceWriter in traceWriterList)
            httpRequestMessage.Headers.Add(traceWriter.Name, traceWriter.GetValue());

        httpRequestMessage.Content = PrepairContent(requestData.Body, requestData.ContentType);

        var responseMessage = await httpConnectionService.SendRequestPollyServiceAsync(httpRequestMessage, httpClient, default);

        if (!responseMessage.IsSuccessStatusCode)
            return new HttpResponse<TResponse> 
            { 
                Headers = responseMessage.Headers, 
                ContentHeaders = responseMessage.Content.Headers,
                StatusCode = responseMessage.StatusCode
            };

        return new HttpResponse<TResponse> 
        {
            Body = JsonConvert.DeserializeObject<TResponse>(await responseMessage.Content.ReadAsStringAsync()),
            Headers = responseMessage.Headers,
            ContentHeaders = responseMessage.Content.Headers,
            StatusCode = responseMessage.StatusCode
        };
    }

    private static HttpContent PrepairContent(object body, ContentType contentType)
    {
        switch (contentType)
        {
            case ContentType.ApplicationJson:
                {
                    if (body is string stringBody)
                    {
                        body = JToken.Parse(stringBody);
                    }

                    var serializeSettings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    var serializedBody = JsonConvert.SerializeObject(body, serializeSettings);
                    var content = new StringContent(serializedBody, Encoding.UTF8, MediaTypeNames.Application.Json);
                    return content;
                }

            case ContentType.XWwwFormUrlEncoded:
                {
                    if (body is not IEnumerable<KeyValuePair<string, string>> list)
                    {
                        throw new Exception(
                            $"Body for content type {contentType} must be {typeof(IEnumerable<KeyValuePair<string, string>>).Name}");
                    }

                    return new FormUrlEncodedContent(list);
                }
            case ContentType.ApplicationXml:
                {
                    if (body is not string s)
                    {
                        throw new Exception($"Body for content type {contentType} must be XML string");
                    }

                    return new StringContent(s, Encoding.UTF8, MediaTypeNames.Application.Xml);
                }
            case ContentType.Binary:
                {
                    if (body.GetType() != typeof(byte[]))
                    {
                        throw new Exception($"Body for content type {contentType} must be {typeof(byte[]).Name}");
                    }

                    return new ByteArrayContent((byte[])body);
                }
            case ContentType.TextXml:
                {
                    if (body is not string s)
                    {
                        throw new Exception($"Body for content type {contentType} must be XML string");
                    }

                    return new StringContent(s, Encoding.UTF8, MediaTypeNames.Text.Xml);
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null);
        }
    }
}