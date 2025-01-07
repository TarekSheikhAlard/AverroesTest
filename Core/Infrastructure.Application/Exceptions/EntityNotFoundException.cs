using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {

        public EntityNotFoundException(string entityName, string id)
            : base($"{entityName} with ID {id} not found")
        {
        }

        public EntityNotFoundException(string entityName, string comparer, string value)
            : base($"{entityName} with {comparer} {value} not found")
        {
        }
    }
}
