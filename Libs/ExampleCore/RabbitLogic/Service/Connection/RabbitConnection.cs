using System.Collections.Concurrent;
using RabbitMQ.Client;

namespace ExampleCore.RabbitLogic.Service.Connection;

public class RabbitConnection
{
    private readonly ConcurrentBag<IConnection> connections = [];
    private readonly ConnectionFactory connectionFactory = new() { HostName = "localhost", Port = 5672};
    
    public IConnection Get()
    {
        return connections.TryTake(out var connection) ? connection : 
            connectionFactory.CreateConnection();
    }

    public void Return(IConnection connection)
    {
        connections.Add(connection);
    }
}