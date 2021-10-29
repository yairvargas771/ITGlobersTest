using Domain.Libreria;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Libreria.Repositories
{
    public class LibroRepository : Repository<int, Libro>
    {
        public LibroRepository(DbContext persistenceContext) : base(persistenceContext)
        {
        }
    }
}
