using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Libreria;
using ITGlobersTest.Services;

namespace ITGlobersTest.Pages.Editoriales
{
    public class CreateModel : PageModel
    {
        private readonly EditorialService editorialService;
        private readonly LibroService libroService;

        public CreateModel(EditorialService autorService, LibroService libroService)
        {
            this.editorialService = autorService;
            this.libroService = libroService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var libros = await libroService.GetLibrosAsync();
            ViewData["libros"] = libros.ToList();

            return Page();
        }

        [BindProperty]
        public Editorial Editorial { get; set; }
        [BindProperty]
        public int selectedLibro { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await editorialService.SaveEditorialAsync(Editorial, selectedLibro);

            return RedirectToPage("./Index");
        }
    }
}
