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

namespace ITGlobersTest.Pages.Editoriales
{
    public class EditModel : PageModel
    {
        private readonly EditorialService editorialService;

        public EditModel(EditorialService editorialService)
        {
            this.editorialService = editorialService;
        }

        [BindProperty]
        public Categoria Editorial { get; set; }
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

            Editorial = await editorialService.GetEditorialAsync(id.Value);

            if (Editorial == null)
            {
                return NotFound();
            }

//            ViewData["libros"] = libros.Where(libro => !Editorial.Libros.Where(_libro => _libro.Id == libro.Id).Any()).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            await editorialService.UpdateEditorialAsync(Editorial);
            if (selectedLibroAEliminar != 0)
            {
                await editorialService.DesasociarLibro(selectedLibroAEliminar, Editorial.Id);
            }

            if (selectedLibroAAgregar != 0)
            {
                await editorialService.AsociarLibro(selectedLibroAAgregar, Editorial.Id);
            }

            await editorialService.UpdateEditorialAsync(Editorial);

            return RedirectToPage("./Index");
        }
    }
}
