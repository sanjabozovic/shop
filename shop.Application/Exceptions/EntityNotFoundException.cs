using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type type)
            : base($"Entity of type {type.Name} was not found.")
        {

        }
    }
}
