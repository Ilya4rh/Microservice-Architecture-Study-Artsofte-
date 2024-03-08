using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Interfaces;

namespace Services;

public static class ServiceStartUp
{
    public static IServiceCollection TryAddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IPostService, PostService>();
        serviceCollection.TryAddScoped<IAnswerService, AnswerService>();
        serviceCollection.TryAddScoped<ICommentService, CommentService>();
        return serviceCollection;
    }
}
