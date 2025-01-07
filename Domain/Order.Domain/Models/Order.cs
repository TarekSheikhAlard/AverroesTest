using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Domain.Models;

namespace Order.Domain.Models
{
    public class Order : BasicEntityWithAuditInfo
    {
        [MaxLength(50)]
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
