using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Libreria
{
    public class Libro : Entity<int>
    {
        [Column("ISBN")]
        public override int Id { get; set; }
        public Editorial Editorial { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }

        [Column("n_paginas")]
        public string Paginas { get; set; }
        public IList<Autor> Autores {get;set;}

        public Libro() { }

        public static explicit operator Libro(LibroDto libroDto)
        {
            if (libroDto.Editorial != null)
                libroDto.Editorial.Libros = null;

            if (libroDto.Autores != null)
                libroDto.Autores = libroDto.Autores.Select(autor => { autor.Libros = null; return autor; }).ToList();

            return new Libro()
            {
                Id = libroDto.Id,
                Autores = libroDto.Autores,
                Editorial = libroDto.Editorial,
                Paginas = libroDto.Paginas,
                Sinopsis = libroDto.Sinopsis,
                Titulo = libroDto.Titulo
            };
        }
    }
}
