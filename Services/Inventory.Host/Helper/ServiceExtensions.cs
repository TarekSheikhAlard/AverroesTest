using AutoMapper;
using Inventory.Application.InventoryAppService.Validations;
using Inventory.Application.InventoryAppService;
using Sieve.Services;
using Inventory.Application.InventoryAppService.Dtos;
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
            services.AddTransient<IInventoryAppService, Application.InventoryAppService.InventoryAppService>();
            services.AddTransient<InventoryValidator>();
            services.AddScoped<ISieveProcessor, SieveProcessor>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new InventoryMapperProfile());
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
            IChannel channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: "inventory_exchange", type: "direct");
            await channel.QueueBindAsync(queue: "notification", exchange: "inventory_exchange", routingKey: "notification.created");


            services.AddSingleton(connection);
            services.AddSingleton(channel);
            services.AddSingleton(settings);
            services.AddSingleton<RabbitMQPublisher>();


        }
    }
}
