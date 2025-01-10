using Infrastructure.Application.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Infrastructure.Application.MessageBroker;
using Inventory.Application.InventoryAppService.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application.InventoryAppService.EventListener
{
    public class InventoryConsumerService
    {
        private readonly IConnection _connection;
        private readonly IServiceProvider _serviceProvider;


        public InventoryConsumerService(IConnection connection, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _serviceProvider = serviceProvider;
           
        }
        public async Task ExecuteAsync()
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


            await channel.BasicConsumeAsync(
                queue: "order",
                autoAck: false,
                consumer: consumer
            );



        }
    }
}
