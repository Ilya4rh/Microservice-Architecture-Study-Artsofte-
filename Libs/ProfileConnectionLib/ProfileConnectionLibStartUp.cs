using Microsoft.Extensions.DependencyInjection;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;

namespace ProfileConnectionLib;

public static class ProfileConnectionLibStartUp
{
    public static IServiceCollection TryAddProfileConnectionServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IConnectionService, ConnectionService>();
        return serviceCollection;
    }
}
