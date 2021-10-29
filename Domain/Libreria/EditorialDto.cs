using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Libreria
{
    public class EditorialDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Sede { get; set; }
        public IList<Libro> Libros { get; set; }

        public static explicit operator EditorialDto(Editorial editorial)
        {
            if (editorial.Libros != null)
                editorial.Libros = editorial.Libros.Select(libro => { libro.Editorial = null; return libro; }).ToList();

            return new EditorialDto()
            {
                Id = editorial.Id,
                Libros = editorial.Libros,
                Nombre = editorial.Nombre,
                Sede = editorial.Sede
            };
        }
    }
}
