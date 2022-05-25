using Domain.Libreria;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Categorias.Repositories
{
    public class ProductoRepository : Repository<int, Producto>
    {
        public ProductoRepository(DbContext persistenceContext) : base(persistenceContext)
        {
        }
    }
}
