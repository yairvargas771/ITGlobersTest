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
    public class DetailsModel : PageModel
    {
        private readonly LibroService libroService;

        public DetailsModel(LibroService libroService)
        {
            this.libroService = libroService;
        }

        public Libro Libro { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Libro = await libroService.GetLibroAsync(id.Value);

            if (Libro == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
