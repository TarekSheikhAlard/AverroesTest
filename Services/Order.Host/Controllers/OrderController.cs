using Microsoft.AspNetCore.Mvc;
using Infrastructure.Service.Controllers;
using Order.Application.OrderAppService;
using Order.Application.OrderAppService.Dtos;
using Sieve.Models;
using Microsoft.AspNetCore.Authorization;

namespace Order.Host.Controllers
{
    [Authorize]
    public class OrderController : BaseController<IOrderAppService, Domain.Models.Order, OrderGetDto, OrderCreateDto, OrderUpdateDto, SieveModel>
    {
        IOrderAppService _appService;
        public OrderController(IOrderAppService appService) : base(appService)
        {
            _appService = appService;
        }


    }

}
