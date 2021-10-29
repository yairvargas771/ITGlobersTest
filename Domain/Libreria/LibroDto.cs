using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Libreria
{
    public class LibroDto
    {
        public int Id { get; set; }
        public Editorial Editorial { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public string Paginas { get; set; }
        public IList<Autor> Autores { get; set; }

        public static explicit operator LibroDto(Libro libro)
        {
            if (libro.Editorial != null)
                libro.Editorial.Libros = null;

            if (libro.Autores != null)
                libro.Autores = libro.Autores.Select(autor => { autor.Libros = null; return autor; }).ToList();

            return new LibroDto()
            {
                Id = libro.Id,
                Autores = libro.Autores,
                Editorial = libro.Editorial,
                Paginas = libro.Paginas,
                Sinopsis = libro.Sinopsis,
                Titulo = libro.Titulo
            };
        }
    }
}
