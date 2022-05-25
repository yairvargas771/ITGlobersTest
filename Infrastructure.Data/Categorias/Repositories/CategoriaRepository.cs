using Domain.Libreria;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Categorias.Repositories
{
    public class CategoriaRepository : Repository<int, Categoria>
    {
        public CategoriaRepository(DbContext persistenceContext) : base(persistenceContext)
        {
        }
    }
}
