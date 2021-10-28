using System.Collections.Generic;

namespace Domain.Libreria
{
    public class Autor : Entity<int>
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public IList<Libro> Libros { get; set; }

        public Autor() { }
    }
}
