using MassTransit;
using ProfileConnectionLib.SagaLogic.Service.Models;

namespace ProfileConnectionLib.SagaLogic.Service.Interfaces;

public interface ISagaConsumer
{
    Task Consume(ConsumeContext<CreatePostSagaRequest> context);
}