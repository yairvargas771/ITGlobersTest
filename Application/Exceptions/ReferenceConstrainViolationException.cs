using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ReferenceConstrainViolationException : Exception
    {
        public ReferenceConstrainViolationException(Type parent, Type child) : base("Este(a) " + parent.Name + " no puede eliminar porque tiene asociado uno(a) o varios " + child.Name + "(s)") { }
    }
}
