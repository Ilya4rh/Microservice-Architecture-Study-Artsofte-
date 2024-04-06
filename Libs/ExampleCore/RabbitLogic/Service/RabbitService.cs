using System.Collections.Concurrent;
using System.Text;
using ExampleCore.RabbitLogic.Service.Connection;
using ExampleCore.RabbitLogic.Service.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExampleCore.RabbitLogic.Service;

public class RabbitService : IRabbitService
{
    private readonly IModel channel;
    private readonly IConnection connection;
    private readonly RabbitConnection rabbitConnection;
    private readonly HashSet<string> queues = [];
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper = new();

    public RabbitService(RabbitConnection rabbitConnection)
    {
        this.rabbitConnection = rabbitConnection;
        connection = rabbitConnection.Get();
        channel = connection.CreateModel();
    }

    private void CreateAndListenReplyQueue(string queueName, string queueRoutingKey)
    {
        channel.ExchangeDeclare("rpc_exc", ExchangeType.Direct);
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName, "rpc_exc", queueRoutingKey, null);
        queues.Add(queueName);
        
        var consumer = new EventingBasicConsumer(channel);
        
        consumer.Received += (model, ea) =>
        {
            if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var taskCompletionSource))
                return;
            
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            
            taskCompletionSource.TrySetResult(response);
        };

        channel.BasicConsume(consumer, queueName, true);
    }

    public Task<string> SendMessage(string message, string replyQueueName  = "checkUserExistReply", 
        string replyQueueRoutingKey  = "checkUserExistReply", string publishRoutingKey = "checkUserExist", 
        CancellationToken cancellationToken = default)
    {
        if (!queues.Contains(replyQueueName))
            CreateAndListenReplyQueue(replyQueueName, replyQueueRoutingKey);
        
        var props = channel.CreateBasicProperties();
        var correlationId = Guid.NewGuid().ToString();
        
        props.CorrelationId = correlationId;
        props.ReplyTo = replyQueueName;
        
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var taskCompletionSource = new TaskCompletionSource<string>();
        
        callbackMapper.TryAdd(correlationId, taskCompletionSource);
        channel.BasicPublish("rpc_exc", publishRoutingKey, props, messageBytes);
        cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));
        
        return taskCompletionSource.Task;
    }

    public void Dispose()
    {
        channel.Close();
        rabbitConnection.Return(connection);
    }
}