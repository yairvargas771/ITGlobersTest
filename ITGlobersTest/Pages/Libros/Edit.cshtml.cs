using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Libreria;
using ITGlobersTest.Services;

namespace ITGlobersTest.Pages.Libros
{
    public class EditModel : PageModel
    {
        private readonly LibroService libroService;

        public EditModel(LibroService libroService)
        {
            this.libroService = libroService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await libroService.UpdateLibroAsync(Libro);

            return RedirectToPage("./Index");
        }

        private async Task<bool> LibroExistsAsync(int id)
        {
            return (await libroService.GetLibroAsync(id)) != null;
        }
    }
}
