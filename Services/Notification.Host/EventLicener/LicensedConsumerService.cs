using Infrastructure.Application.Configuration;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sieve.Extensions.MethodInfoExtended;
using System.Text.Json;

namespace Inventory.Host.InventoryAppService.EventLicener
{
    public class LicensedConsumerService : BackgroundService
    {
        private readonly IChannel _channel;
        private readonly RabbitMQSettings _settings;

        private readonly IServiceProvider _serviceProvider;


        public LicensedConsumerService(IChannel channel)
        {
            _channel = channel;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                var consumer = new AsyncEventingBasicConsumer(_channel);


                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Email Send: {message}");

                    using var scope = _serviceProvider.CreateScope();
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);


                };

                while (!stoppingToken.IsCancellationRequested)
                { 
                    await _channel.BasicConsumeAsync(
                        queue: "notification",        
                        autoAck: false,       
                        consumer: consumer
                    );

                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }

        }
    }
}
