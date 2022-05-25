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
    public class IndexModel : PageModel
    {
        private readonly EditorialService editorialService;

        public IndexModel(EditorialService editorialService)
        {
            this.editorialService = editorialService;
        }

        public IList<Categoria> Editorial { get; set; }

        public async Task OnGetAsync()
        {
            Editorial = (await editorialService.GetEditorialesAsync()).ToList();
        }
    }
}
