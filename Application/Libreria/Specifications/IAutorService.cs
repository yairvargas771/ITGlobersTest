using Domain.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Libreria.Specifications
{
    public interface IAutorService
    {
        Task<Producto> GetAutorAsync(int id, bool eager = false);
        Task<Producto> GetAutorAsync(Expression<Func<Producto, bool>> cond, bool eager = false);
        Task<IEnumerable<Producto>> GetAllAutoresAsync(bool eager = false);
        Task<IEnumerable<Producto>> GetAutoresAsync(Expression<Func<Producto, bool>> cond, bool eager = false);
        Task UpdateAutorAsync(Producto autor);
        Task DeleteAutorAsync(int id);
        Task DeleteAutorAsync(Expression<Func<Producto, bool>> cond);
        Task<Producto> CreateAutorAsync(Producto autor);
        Task AgregarLibroAsync(int isbn, int autorId);
        Task RemoverLibroAsync(int isbn, int autorId);
    }
}
