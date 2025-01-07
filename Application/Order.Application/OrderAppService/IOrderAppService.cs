using Infrastructure.Application;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.OrderAppService.Dtos;

namespace Order.Application.OrderAppService
{
    public interface IOrderAppService : IBaseAppService<OrderGetDto, OrderCreateDto, OrderUpdateDto, SieveModel>
    {
    }
}
