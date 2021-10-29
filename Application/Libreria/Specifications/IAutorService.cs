using Domain.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Libreria.Specifications
{
    public interface IAutorService
    {
        Task<Autor> GetAutorAsync(int id, bool eager = false);
        Task<Autor> GetAutorAsync(Expression<Func<Autor, bool>> cond, bool eager = false);
        Task<IEnumerable<Autor>> GetAllAutoresAsync(bool eager = false);
        Task<IEnumerable<Autor>> GetAutoresAsync(Expression<Func<Autor, bool>> cond, bool eager = false);
        Task UpdateAutorAsync(Autor autor);
        Task DeleteAutorAsync(int id);
        Task DeleteAutorAsync(Expression<Func<Autor, bool>> cond);
        Task<Autor> CreateAutorAsync(Autor autor);
        Task AgregarLibroAsync(int isbn, int autorId);
        Task RemoverLibroAsync(int isbn, int autorId);
    }
}
