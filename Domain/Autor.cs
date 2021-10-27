using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Autor : Entity<int>
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public IEnumerable<Book> Books { get; set; }

        private Autor() { }
    }
}
