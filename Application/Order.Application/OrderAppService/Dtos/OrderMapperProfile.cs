using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Order.Application.OrderAppService.Dtos
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<Domain.Models.Order, OrderGetDto>();
            CreateMap<OrderCreateDto, Domain.Models.Order>();
            CreateMap<OrderUpdateDto, Domain.Models.Order>();
        }
    }
}
