using System;
using System.Collections.Generic;

namespace Domain
{
    public class Book : Entity<int>
    {
        public int ISBN { get; set; }
        public Editorial Editorial { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public string Paginas { get; set; }
        public IEnumerable<Autor> Autores {get;set;}

        private Book() { }
    }
}
