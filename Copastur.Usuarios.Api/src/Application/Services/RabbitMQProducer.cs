using System.Text;
using System.Text.Json;

using Application.Interfaces;

using Microsoft.Extensions.Configuration;

using RabbitMQ.Client;

namespace Application.Services;

public class RabbitMQProducer : IRabbitMQProducer
{
    private readonly IConfiguration _configuration;

    public RabbitMQProducer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendAuditMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_configuration.GetConnectionString("Rabbitmq"))
        };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare("auditoria", exclusive: false);
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: "auditoria", body: body);
            }
        }
    }
}
