using ExampleCore.HttpLogic.Polly;
using ExampleCore.HttpLogic.Polly.Interfaces;
using ExampleCore.HttpLogic.Services.Connection.Service;
using ExampleCore.HttpLogic.Services.Interfaces;
using ExampleCore.HttpLogic.Services.Requests.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExampleCore.HttpLogic;

/// <summary>
/// Регистрация в DI сервисов для HTTP-соединений
/// </summary>
public static class HttpServiceStartup
{
    /// <summary>
    /// Добавление сервиса для осуществления запросов по HTTP
    /// </summary>
    public static IServiceCollection TryAddHttpRequestService(this IServiceCollection services)
    {
        services
                .AddHttpContextAccessor()
                .AddHttpClient()
                .AddTransient<IHttpConnectionService, HttpConnectionService>()
                .AddTransient<IPollyService, PollyService>();

        services.TryAddTransient<IHttpRequestService, HttpRequestService>();

        return services;
    }
}