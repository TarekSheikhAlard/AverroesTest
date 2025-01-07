using AutoMapper;
using Sieve.Services;
using Infrastructure.Application.Configuration;
using RabbitMQ.Client;
using Inventory.Host.InventoryAppService.EventLicener;
using Infrastructure.Application.MessageBroker;

namespace Inventory.Host.Helper
{

    public static class ServiceExtensions
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<ISieveProcessor, SieveProcessor>();

        }

        public static async Task AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();

            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = settings.Host,
                Port = settings.Port,
                UserName = settings.Username,
                Password = settings.Password
                
            };
            
            var connection = await factory.CreateConnectionAsync();
            IChannel channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: "inventory_exchange", type: "direct");
            await channel.QueueDeclareAsync(queue: "notification", durable: true, exclusive: false, autoDelete: false, arguments: null);
            await channel.QueueBindAsync(queue: "notification", exchange: "inventory_exchange", routingKey: "notification.created");


            services.AddSingleton(connection);
            services.AddSingleton(channel);
            services.AddSingleton(settings);


        }
    }
}
