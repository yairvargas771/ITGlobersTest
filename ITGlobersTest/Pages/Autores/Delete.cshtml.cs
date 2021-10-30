using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Libreria;
using ITGlobersTest.Services;

namespace ITGlobersTest.Pages.Autores
{
    public class DeleteModel : PageModel
    {
        private readonly AutorService autorService;

        public DeleteModel(AutorService autorService)
        {
            this.autorService = autorService;
        }

        [BindProperty]
        public Autor Autor { get; set; }
        public string Message { get; set; } = "";

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Autor = await autorService.GetAutorAsync(id.Value);

            if (Autor != null)
            {
                Message = await autorService.DeleteAutorAsync(id.Value);
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
