using Domain.Libreria;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Libreria.Repositories
{
    public class EditorialRepository : Repository<int, Editorial>
    {
        public EditorialRepository(DbContext persistenceContext) : base(persistenceContext)
        {
        }
    }
}
