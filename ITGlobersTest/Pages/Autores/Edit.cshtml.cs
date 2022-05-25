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

namespace ITGlobersTest.Pages.Autores
{
    public class EditModel : PageModel
    {
        private readonly AutorService autorService;

        public EditModel(AutorService autorService)
        {
            this.autorService = autorService;
        }

        [BindProperty]
        public Producto Autor { get; set; }
        [BindProperty]
        public int selectedLibroAEliminar { get; set; }
        [BindProperty]
        public int selectedLibroAAgregar { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Autor = await autorService.GetAutorAsync(id.Value);

            if (Autor == null)
            {
                return NotFound();
            }

            // ViewData["libros"] = libros.Where(libro => !Autor.Libros.Where(_libro => _libro.Id == libro.Id).Any()).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            await autorService.UpdateAutorAsync(Autor);
            if (selectedLibroAEliminar != 0)
            {
                await autorService.DesasociarLibro(selectedLibroAEliminar, Autor.Id);
            }

            if (selectedLibroAAgregar != 0)
            {
                await autorService.AsociarLibro(selectedLibroAAgregar, Autor.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
