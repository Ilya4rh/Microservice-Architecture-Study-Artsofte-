using MassTransit;

namespace ProfileConnectionLib.SagaLogic.Service;

public class SagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    
    public Guid UserId { get; set; }

    public Guid PostId { get; set; }

    public State? State { get; set; }
}