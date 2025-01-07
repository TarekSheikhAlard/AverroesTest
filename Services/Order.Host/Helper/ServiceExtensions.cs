using AutoMapper;
using Infrastructure.Application.Configuration;
using Infrastructure.Application.MessageBroker;
using Microsoft.AspNetCore.Identity;
using Order.Application.OrderAppService;
using Order.Application.OrderAppService.Dtos;
using Order.Application.OrderAppService.Validations;
using RabbitMQ.Client;
using Sieve.Services;

namespace Order.Host.Helper
{
    public static class ServiceExtensions
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddTransient<IOrderAppService, OrderAppService>();

            services.AddTransient<OrderValidator>();

            services.AddScoped<ISieveProcessor, SieveProcessor>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new OrderMapperProfile());

            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

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
             var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: "inventory_exchange", type: "direct");
            await channel.QueueDeclareAsync(queue: "order",durable: true, exclusive: false, autoDelete: false, arguments: null);
            await channel.QueueBindAsync(queue: "order", exchange: "inventory_exchange", routingKey: "order.created");

            services.AddSingleton(connection);
            services.AddSingleton(channel);
            services.AddSingleton(settings);
            services.AddSingleton<RabbitMQPublisher>();
        }
    }
}
