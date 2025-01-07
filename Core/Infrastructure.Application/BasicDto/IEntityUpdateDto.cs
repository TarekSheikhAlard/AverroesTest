using Infrastructure.Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Application.BasicDto
{
    public class IEntityUpdateDto : IValidatableDto
    {
        public int Id { get; set; }
    }
}
