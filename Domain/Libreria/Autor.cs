using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Domain.Libreria
{
    public class Autor : Entity<int>
    {
        [DisplayName("Nombre del autor")]
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public IList<Libro> Libros { get; set; }

        public Autor() { }

        public static explicit operator Autor(AutorDto autorDto)
        {
            if (autorDto.Libros != null)
                autorDto.Libros = autorDto.Libros.Select(libro => { libro.Autores = null; return libro; }).ToList();

            return new Autor()
            {
                Id = autorDto.Id,
                Nombre = autorDto.Nombre,
                Apellidos = autorDto.Apellidos,
                Libros = autorDto.Libros
            };
        }
    }
}
