using Domain.Interfaces;
using Infastractes.Connections;
using Infastractes.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infastractes;

public static class InfastractesStartUp
{
    public static IServiceCollection TryAddInfastractes(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IPostRepository, PostRepository>();
        serviceCollection.TryAddScoped<IAnswerRepository, AnswerRepository>();
        serviceCollection.TryAddScoped<ICommentRepository, CommentRepository>();
        serviceCollection.TryAddScoped<ICheckUserExist, CheckUserExist>();
        return serviceCollection;
    }
}
