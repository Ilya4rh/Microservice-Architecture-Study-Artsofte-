using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using ProfileConnectionLib.ConnectionServices.RequestService;
using ProfileConnectionLib.ConnectionServices.RequestService.Interfaces;

namespace ProfileConnectionLib;

public static class ProfileConnectionLibStartUp
{
    public static IServiceCollection TryAddProfileConnectionServices(this IServiceCollection serviceCollection, 
        IConfiguration configuration)
    {
        if (configuration.GetSection("RequestsType").Value == "http")
            serviceCollection.AddScoped<IRequestService, HttpRequestService>();
        else
            serviceCollection.AddScoped<IRequestService, RabbitRequestService>();
        
        serviceCollection.AddScoped<IConnectionService, ConnectionService>();
        return serviceCollection;
    }
}