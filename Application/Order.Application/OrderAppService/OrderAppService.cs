using AutoMapper;
using Infrastructure.Application;
using Infrastructure.Application.MessageBroker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Order.Application.OrderAppService.Dtos;
using Order.Application.OrderAppService.Validations;
using Order.Data.DbContext;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.OrderAppService
{
    public class OrderAppService : BaseAppService<OrderDbContext, Domain.Models.Order, OrderGetDto, OrderCreateDto, OrderUpdateDto, SieveModel>, IOrderAppService
    {
        IHttpContextAccessor _httpContextAccessor;
        OrderDbContext _serviceDbContext;
        RabbitMQPublisher _publisher;
        public OrderAppService(OrderDbContext serviceDbContext, IMapper mapper, ISieveProcessor processor, OrderValidator validations, IHttpContextAccessor httpContextAccessor, RabbitMQPublisher publisher) : base(serviceDbContext, mapper, processor, validations, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceDbContext = serviceDbContext;
            _publisher = publisher;
        }

        public override async Task<OrderGetDto> Create(OrderCreateDto create)
        {
            _publisher.Publish("order.created", "inventory_exchange", create);
            return await base.Create(create);
        }

        protected override IQueryable<Domain.Models.Order> QueryExcuter(SieveModel input)
        {
            return base.QueryExcuter(input);
        }
    }
}
