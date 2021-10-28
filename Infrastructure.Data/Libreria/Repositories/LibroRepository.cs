using Domain.Libreria;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Libreria.Repositories
{
    public class LibroRepository : Repository<int, Libro>
    {
        public LibroRepository(DbContext persistenceContext) : base(persistenceContext)
        {
        }
    }
}
