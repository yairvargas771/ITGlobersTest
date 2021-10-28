using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EntityIsAlreadyRelatedToException : Exception
    {
        public EntityIsAlreadyRelatedToException(Type parent, Type child) : base("Este(a) " + parent.Name + " ya tiene un " + child.Name + " con la misma llave asociado(a)") { }
    }
}
