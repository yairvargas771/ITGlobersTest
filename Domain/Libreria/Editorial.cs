using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Domain.Libreria
{
    public class Editorial : Entity<int>
    {
        [DisplayName("Nombre de la editorial")]
        public string Nombre { get; set; }
        public string Sede { get; set; }
        public IList<Libro> Libros { get; set; }

        public Editorial() { }

        public static explicit operator Editorial(EditorialDto editorialDto)
        {
            if (editorialDto.Libros != null)
                editorialDto.Libros = editorialDto.Libros.Select(libro => { libro.Editorial = null; return libro; }).ToList();

            return new Editorial()
            {
                Id = editorialDto.Id,
                Libros = editorialDto.Libros,
                Nombre = editorialDto.Nombre,
                Sede = editorialDto.Sede
            };
        }
    }
}