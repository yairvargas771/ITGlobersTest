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
    public class DetailsModel : PageModel
    {
        private readonly AutorService autorService;

        public DetailsModel(AutorService autorService)
        {
            this.autorService = autorService;
        }

        public Producto Autor { get; set; }

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
    }
}
