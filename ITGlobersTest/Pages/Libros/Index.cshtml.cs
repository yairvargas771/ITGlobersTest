using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Libreria;
using ITGlobersTest.Services;

namespace ITGlobersTest.Pages.Libros
{
    public class IndexModel : PageModel
    {
        private readonly LibroService libroService;

        public IndexModel(LibroService libroService)
        {
            this.libroService = libroService;
        }

        public IList<Libro> Libro { get; set; }

        public async Task OnGetAsync()
        {
            Libro = (await libroService.GetLibrosAsync()).ToList();
        }
    }
}
