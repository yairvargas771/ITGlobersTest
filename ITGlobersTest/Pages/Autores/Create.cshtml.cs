using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Libreria;
using ITGlobersTest.Services;

namespace ITGlobersTest.Pages.Autores
{
    public class CreateModel : PageModel
    {
        private readonly AutorService autorService;
        private readonly LibroService libroService;

        public CreateModel(AutorService autorService, LibroService libroService)
        {
            this.autorService = autorService;
            this.libroService = libroService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var libros = await libroService.GetLibrosAsync();
            ViewData["libros"] = libros.ToList();

            return Page();
        }

        [BindProperty]
        public Autor Autor { get; set; }
        [BindProperty]
        public int selectedLibro { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await autorService.SaveAutorAsync(Autor, selectedLibro);

            return RedirectToPage("./Index");
        }
    }
}
