using Infrastructure.Application.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Application.MessageBroker
{
    public class RabbitMQPublisher
    {
        private readonly IChannel _channel;
        private readonly RabbitMQSettings _settings;

        public RabbitMQPublisher(IChannel channel, RabbitMQSettings settings)
        {
            _channel = channel;
            _settings = settings;
        }

        public async void Publish(string routingKey,string exchange, object message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await _channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey,
                body: body
            );

            Console.WriteLine($"Message sent: {routingKey}");
        }
    }
}
