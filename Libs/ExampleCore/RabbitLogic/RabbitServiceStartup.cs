using ExampleCore.RabbitLogic.Service;
using ExampleCore.RabbitLogic.Service.Connection;
using ExampleCore.RabbitLogic.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleCore.RabbitLogic;

public static class RabbitServiceStartup
{
    public static IServiceCollection TryAddRabbitRequestService(this IServiceCollection services)
    {
        services.AddScoped<IRabbitConsumer, RabbitConsumer>();
        services.AddScoped<IRabbitService, RabbitService>();
        services.AddSingleton<RabbitConnection>();

        return services;
    }
}