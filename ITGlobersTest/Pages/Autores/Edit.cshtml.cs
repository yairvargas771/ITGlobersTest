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
        public Autor Autor { get; set; }
        [BindProperty]
        public int selectedLibro { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await autorService.UpdateAutorAsync(Autor);

            return RedirectToPage("./Index");
        }
    }
}
