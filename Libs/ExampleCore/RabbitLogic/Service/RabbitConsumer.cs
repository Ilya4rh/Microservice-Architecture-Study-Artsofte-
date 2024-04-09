using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ExampleCore.RabbitLogic.Service.Interfaces;

namespace ExampleCore.RabbitLogic.Service;

public class RabbitConsumer : IRabbitConsumer
{
    public EventingBasicConsumer Create(Func<string, Task<string>> onReceiveFunc,  IModel channel,
        string queueName = "checkUserExist", string routingKey = "checkUserExist")
    {
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName, "rpc_exc", routingKey);
        
        var consumer = new EventingBasicConsumer(channel);
        
        consumer.Received += async (model, basicDeliverEventArgs) =>
        {
            var response = string.Empty;
            var body = basicDeliverEventArgs.Body.ToArray();
            var props = basicDeliverEventArgs.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                response = await onReceiveFunc(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
            }
            finally
            {
                var bytes = Encoding.UTF8.GetBytes(response);
                
                channel.BasicPublish(string.Empty, props.ReplyTo, replyProps, bytes);
            }
        };
        channel.BasicConsume(queueName, true, consumer);
        return consumer;
    }
}