using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Application.BasicDto;
using Infrastructure.Application.Validations;

namespace Inventory.Application.InventoryAppService.Dtos
{
    public class InventoryCreateDto : IValidatableDto
    {
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
    }

    public class InventoryUpdateDto : IEntityUpdateDto
    {
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
    }
}
