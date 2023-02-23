using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Application.Services
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private readonly IConfiguration _configuration;

        public RabbitMQConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ReceiveAuditMessage()
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
                    var consumer = new EventingBasicConsumer(channel);
                    var result = channel.BasicConsume("auditoria", true, consumer);

                    return result;
                }
            }
        }
    }
}
