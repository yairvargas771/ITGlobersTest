using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
