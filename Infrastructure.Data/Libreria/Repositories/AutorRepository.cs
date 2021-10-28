using Domain.Libreria;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Libreria.Repositories
{
    public class AutorRepository : Repository<int, Autor>
    {
        public AutorRepository(DbContext persistenceContext) : base(persistenceContext)
        {
        }
    }
}
