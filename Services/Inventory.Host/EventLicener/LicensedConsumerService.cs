using Infrastructure.Application.Configuration;
using Inventory.Application.InventoryAppService;
using Inventory.Application.InventoryAppService.Dtos;
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
using Infrastructure.Application.MessageBroker;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace Inventory.Host.InventoryAppService.EventLicener
{
    public class LicensedConsumerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly RabbitMQSettings _settings;
        private readonly IServiceProvider _serviceProvider;


        public LicensedConsumerService(IConnection connection, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                IChannel channel = await _connection.CreateChannelAsync();
                await channel.QueueBindAsync(queue: "order", exchange: "inventory_exchange", routingKey: "order.created");
                var consumer = new AsyncEventingBasicConsumer(channel);


                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Processed Licensed Event: {message}");
                    var orderDetails = JsonSerializer.Deserialize<OrderDto>(message); 
                    using var scope = _serviceProvider.CreateScope();
                    var inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryAppService>();
                    await inventoryService.ProcessOrder(orderDetails);
                    await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                    RabbitMQPublisher _rabbitMQPublisher = scope.ServiceProvider.GetRequiredService<RabbitMQPublisher>();
                   
                    _rabbitMQPublisher.Publish("notification.created", "inventory_exchange", orderDetails);

              
                    


                };

                while (!stoppingToken.IsCancellationRequested)
                { 
                    await channel.BasicConsumeAsync(
                        queue: "order",        
                        autoAck: false,       
                        consumer: consumer
                    );

                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }

        }
    }
}
