using MassTransit;
using ProfileConnectionLib.SagaLogic.Models;
using ProfileConnectionLib.SagaLogic.Service.Models;

namespace ProfileConnectionLib.SagaLogic.Service;

public class SagaStateMachine : MassTransitStateMachine<SagaState>
{
    public State CreatingTask { get; }
    public State Success { get; }
    public State Failed { get; }

    public Event<CreatePostSagaRequest> CreatePostRequested { get; }
    public Event<CreatePostSagaResponse> PostCreated { get; }
    public Event<CreatePostSagaFaulted> UserNotFound { get; }

    public SagaStateMachine()
    {
        InstanceState(x => x.State);

        Event(() => CreatePostRequested, x => x.CorrelateById(context => context.Message.userId));
        Event(() => UserNotFound, x => x.CorrelateById(context => context.Message.userId));
        Event(() => PostCreated, x => x.CorrelateById(context => context.Message.userId));

        Initially(
            When(CreatePostRequested)
                .Then(context =>
                {
                    context.Instance.UserId = context.Data.userId;
                    context.Instance.PostId = context.Data.postId;
                })
                .Publish(ctx => new CreatePostSagaRequest(ctx.Instance.PostId, ctx.Instance.UserId))
                .TransitionTo(CreatingTask)
        );

        During(CreatingTask,
            When(UserNotFound)
                .Then(context => throw new Exception("Пользователь не найден"))
                .TransitionTo(Failed)
        );

        During(CreatingTask,
            When(PostCreated)
                .TransitionTo(Success)
        );

        SetCompletedWhenFinalized();
    }
}