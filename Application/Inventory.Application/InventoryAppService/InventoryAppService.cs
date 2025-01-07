using AutoMapper;
using Infrastructure.Application;
using Infrastructure.Application.MessageBroker;
using Inventory.Application.InventoryAppService.Dtos;
using Inventory.Application.InventoryAppService.Validations;
using Inventory.Data.DbContext;
using Microsoft.AspNetCore.Http;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.InventoryAppService
{
    public class InventoryAppService : BaseAppService<InventoryDbContext, Domain.Models.Inventory, InventoryGetDto, InventoryCreateDto, InventoryUpdateDto, SieveModel>, IInventoryAppService
    {
        IHttpContextAccessor _httpContextAccessor;
        InventoryDbContext _serviceDbContext;
        public InventoryAppService(InventoryDbContext serviceDbContext, IMapper mapper, ISieveProcessor processor, InventoryValidator validations, IHttpContextAccessor httpContextAccessor) : base(serviceDbContext, mapper, processor, validations, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceDbContext = serviceDbContext;
        }

        public override async Task<InventoryGetDto> Create(InventoryCreateDto create)
        {
            return await base.Create(create);

        }


        public  async Task<OrderDto> ProcessOrder(OrderDto order)
        {
            var product = _serviceDbContext.Inventory.FirstOrDefault(x => x.Code.Equals(order.ProductCode));
            if (product!=null&&product.QuantityInStock >= order.Quantity)
            {
                product.QuantityInStock -= order.Quantity;
                order.OrderStatus = "Done";
                _serviceDbContext.Update(product);
                await _serviceDbContext.SaveChangesAsync();
                return order;
            }
            Console.WriteLine("ProcessOrder");
            order.OrderStatus = "OutOfStock";
            return order;

        }

        protected override IQueryable<Domain.Models.Inventory> QueryExcuter(SieveModel input)
        {
            return base.QueryExcuter(input);
        }
    }
}
