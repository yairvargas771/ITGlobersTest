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
    public class DetailsModel : PageModel
    {
        private readonly EditorialService editorialService;

        public DetailsModel(EditorialService editorialService)
        {
            this.editorialService = editorialService;
        }

        public Categoria Editorial { get; set; }

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
    }
}
