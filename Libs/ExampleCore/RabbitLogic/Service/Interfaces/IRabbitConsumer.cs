using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExampleCore.RabbitLogic.Service.Interfaces;

public interface IRabbitConsumer
{
    public EventingBasicConsumer Create(Func<string, Task<string>> onReceiveFunc, IModel channel,
        string queueName = "checkUserExist", string routingKey = "checkUserExist");
}