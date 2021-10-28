using System.Collections.Generic;

namespace Domain.Libreria
{
    public class Editorial : Entity<int>
    {
        public string Nombre { get; set; }
        public string Sede { get; set; }
        public IList<Libro> Libros { get; set; }

        public Editorial() { }
    }
}