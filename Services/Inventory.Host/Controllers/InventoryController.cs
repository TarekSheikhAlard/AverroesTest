using Microsoft.AspNetCore.Mvc;
using Infrastructure.Service.Controllers;
using Inventory.Application.InventoryAppService;
using Inventory.Application.InventoryAppService.Dtos;
using Sieve.Models;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Host.Controllers
{
    [Authorize]
    public class InventoryController : BaseController<IInventoryAppService, Domain.Models.Inventory, InventoryGetDto, InventoryCreateDto, InventoryUpdateDto, SieveModel>
    {
        IInventoryAppService _appService;
        public InventoryController(IInventoryAppService appService) : base(appService)
        {
            _appService = appService;
        }
    }
}
