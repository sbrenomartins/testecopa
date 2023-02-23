using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Application.Interfaces;

using Domain.DTOs.Requests;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Application.Services
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _model;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQConsumer(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                Uri = new Uri(_configuration.GetConnectionString("Rabbitmq"))
            };

            _connection = factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(
                queue: "auditoria",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_model);

            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonSerializer.Deserialize<AuditoriaMessageDto>(contentString);

                Audit(message);

                _model.BasicAck(eventArgs.DeliveryTag, false);
            };
            
            _model.BasicConsume("auditoria", false, consumer);

            return Task.CompletedTask;
        }

        private void Audit(AuditoriaMessageDto message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var auditoriaService = scope.ServiceProvider.GetRequiredService<IAuditoriaService>();

                auditoriaService.Create(message);
            }
        }
    }
}
