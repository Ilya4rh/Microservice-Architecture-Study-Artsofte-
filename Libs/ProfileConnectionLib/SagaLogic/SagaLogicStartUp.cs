using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ProfileConnectionLib.SagaLogic.Service;
using ProfileConnectionLib.SagaLogic.Service.Interfaces;

namespace ProfileConnectionLib.SagaLogic;

public static class SagaLogicStartUp
{
    public static IServiceCollection TryAddSagaLogic(this IServiceCollection services)
    {
        services.AddScoped<ISagaConsumer, SagaConsumer>();
        services.AddSingleton<SagaState>();
        services.AddSingleton<SagaStateMachine>();
        services.AddDbContext<SagaDataBaseContext>();
        
        return services;
    }
}