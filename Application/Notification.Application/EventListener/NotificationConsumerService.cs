using Infrastructure.Application.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Notification.Application.EventListener
{
    public class NotificationConsumerService
    {
        private readonly IChannel _channel;
        private readonly RabbitMQSettings _settings;

        private readonly IServiceProvider _serviceProvider;


        public  NotificationConsumerService(IChannel channel)
        {
            _channel = channel;
             
        }
        public async Task ExecuteAsync()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);


            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Email Send: {message}");


                await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);


            };

            await _channel.BasicConsumeAsync(
                queue: "notification",
                autoAck: false,
                consumer: consumer
            );



        }
    }
}
