using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Libreria;
using ITGlobersTest.Services;

namespace ITGlobersTest.Pages.Editoriales
{
    public class DeleteModel : PageModel
    {
        private readonly EditorialService editorialService;

        public DeleteModel(EditorialService editorialService)
        {
            this.editorialService = editorialService;
        }

        [BindProperty]
        public Categoria Editorial { get; set; }
        public string Message { get; set; } = "";

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Editorial = await editorialService.GetEditorialAsync(id.Value);

            if (Editorial != null)
            {
                Message = await editorialService.DeleteEditorialAsync(id.Value);
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
