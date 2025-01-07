using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.InventoryAppService.Dtos
{
    public class InventoryMapperProfile : Profile
    {
        public InventoryMapperProfile()
        {
            CreateMap<Domain.Models.Inventory, InventoryGetDto>();
            CreateMap<InventoryCreateDto, Domain.Models.Inventory>();
            CreateMap<InventoryUpdateDto, Domain.Models.Inventory>();
        }
    }
}
