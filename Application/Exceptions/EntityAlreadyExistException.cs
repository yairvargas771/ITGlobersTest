using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EntityAlreadyExistException : Exception
    {
        public EntityAlreadyExistException(Type type) : base("Este(a) " + type.Name + " ya está registrada") { }
    }
}
