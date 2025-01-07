using Infrastructure.Application.BasicDto;
using Infrastructure.Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.OrderAppService.Dtos
{
    public class OrderCreateDto : IValidatableDto
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderUpdateDto : IEntityUpdateDto
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}