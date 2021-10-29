using System.Collections.Generic;
using System.Linq;

namespace Domain.Libreria
{
    public class AutorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public IList<Libro> Libros { get; set; }

        public static explicit operator AutorDto(Autor autor)
        {
            if (autor.Libros != null)
                autor.Libros = autor.Libros.Select(libro => { libro.Autores = null; return libro; }).ToList();

            return new AutorDto()
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
                Apellidos = autor.Apellidos,
                Libros = autor.Libros
            };
        }
    }
}
