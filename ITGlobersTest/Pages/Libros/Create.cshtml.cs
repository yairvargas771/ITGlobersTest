using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Libreria;
using ITGlobersTest.Services;

namespace ITGlobersTest.Pages.Libros
{
    public class CreateModel : PageModel
    {
        private readonly LibroService libroService;
        private readonly AutorService autorService;
        private readonly EditorialService editorialService;

        public CreateModel(LibroService libroService, EditorialService editorialService, AutorService autorService)
        {
            this.libroService = libroService;
            this.editorialService = editorialService;
            this.autorService = autorService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var _editoriales = await editorialService.GetEditorialesAsync();
            ViewData["editoriales"] = _editoriales.ToList();

            var _autores = await autorService.GetAutoresAsync();
            ViewData["autores"] = _autores.ToList();
            return Page();
        }

        [BindProperty]
        public Libro Libro { get; set; }

        [BindProperty]
        public int selectedEditorial { get; set; }

        [BindProperty]
        public int selectedAutor { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var libroDto = await libroService.SaveLibroAsync(Libro, selectedAutor, selectedEditorial);

            return RedirectToPage("./Index");
        }
    }
}
