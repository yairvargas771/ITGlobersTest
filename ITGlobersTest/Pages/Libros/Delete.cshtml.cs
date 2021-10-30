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
    public class DeleteModel : PageModel
    {
        private readonly LibroService libroService;

        public DeleteModel(LibroService libroService)
        {
            this.libroService = libroService;
        }

        [BindProperty]
        public Libro Libro { get; set; }
        public string Message { get; set; } = "";

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Libro = await libroService.GetLibroAsync(id.Value);

            if (Libro != null)
            {
                Message = await libroService.DeleteLibroAsync(id.Value);
                if (Message.Length > 0)
                {
                    return Page();
                }
            }
            Message = "";

            return RedirectToPage("./Index");
        }
    }
}
