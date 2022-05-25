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
    public class IndexModel : PageModel
    {
        private readonly AutorService autorService;

        public IndexModel(AutorService autorService)
        {
            this.autorService = autorService;
        }

        public IList<Producto> Autor { get;set; }

        public async Task OnGetAsync()
        {
            Autor = (await autorService.GetAutoresAsync()).ToList();
        }
    }
}
