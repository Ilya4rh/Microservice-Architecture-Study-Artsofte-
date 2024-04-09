namespace ExampleCore.RabbitLogic.Service.Interfaces;

public interface IRabbitService
{
    public Task<string> SendMessage(string message, string replyQueueName  = "checkUserExistReply", 
        string replyQueueRoutingKey  = "checkUserExistReply", string publishRoutingKey = "checkUserExist", 
        CancellationToken cancellationToken = default);
}