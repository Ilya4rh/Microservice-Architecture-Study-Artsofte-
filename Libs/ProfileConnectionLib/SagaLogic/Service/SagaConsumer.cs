using MassTransit;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Requests;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using ProfileConnectionLib.SagaLogic.Models;
using ProfileConnectionLib.SagaLogic.Service.Interfaces;
using ProfileConnectionLib.SagaLogic.Service.Models;

namespace ProfileConnectionLib.SagaLogic.Service;

public class SagaConsumer(IConnectionService connectionService) : ISagaConsumer, IConsumer<CreatePostSagaRequest>
{
    private readonly IConnectionService connectionService = connectionService;
    
    public async Task Consume(ConsumeContext<CreatePostSagaRequest> context)
    {
        try
        {
            var user = await connectionService.CheckUserExistAsync(new CheckUserExistsRequest
            {
                Id = context.Message.userId
            });

            await context.Publish<CreatePostSagaResponse>(new
            {
                context.Message.userId,
                context.Message.postId
            });
        }
        catch (Exception exception)
        {
            await context.Publish<CreatePostSagaFaulted>(new
            {
                context.Message.userId,
                context.Message.postId,
                Reason = exception.Message
            });
        }
    }
}