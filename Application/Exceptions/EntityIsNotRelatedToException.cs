using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EntityIsNotRelatedToException : Exception
    {
        public EntityIsNotRelatedToException(Type owner, Type owned) : base("Este(a) " + owner.Name + " no está relacionado(a) con este " + owned.Name) { }
    }
}
