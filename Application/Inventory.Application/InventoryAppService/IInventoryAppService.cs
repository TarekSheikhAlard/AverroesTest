using Infrastructure.Application;
using Inventory.Application.InventoryAppService.Dtos;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.InventoryAppService
{
    public interface IInventoryAppService : IBaseAppService<InventoryGetDto, InventoryCreateDto, InventoryUpdateDto, SieveModel>

    { 
        public  Task<OrderDto> ProcessOrder(OrderDto order);

    }
}
