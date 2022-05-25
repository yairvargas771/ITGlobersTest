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

        public CreateModel(AutorService autorService)
        {
            this.autorService = autorService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //ViewData["libros"] = libros.ToList();

            return Page();
        }

        [BindProperty]
        public Producto Autor { get; set; }
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
