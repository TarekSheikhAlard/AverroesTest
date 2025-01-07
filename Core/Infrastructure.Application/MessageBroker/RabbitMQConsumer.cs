using Infrastructure.Application.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace Infrastructure.Application.MessageBroker
{
    public class RabbitMQConsumer
    {
        private readonly IChannel _channel;
        private readonly RabbitMQSettings _settings;

        public RabbitMQConsumer(IChannel channel, RabbitMQSettings settings)
        {
            _channel = channel;
            _settings = settings;
        }

        public void Consume(string queueName, Action<string> onMessageReceived)
        {

            Console.WriteLine($"Consuming from queue: {queueName}");
        }
    }

}
