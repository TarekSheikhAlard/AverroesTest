using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inventory.Application.InventoryAppService.Dtos
{
    public class InventoryGetDto
    {
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
    }
}
