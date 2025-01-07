using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Domain.Models;

namespace Inventory.Domain.Models
{
    public class Inventory : BasicEntityWithAuditInfo
    {
        [MaxLength(50)]
        public string ProductName { get; set; }         
        public int QuantityInStock { get; set; }         
        public decimal Price { get; set; }              
    }
}
