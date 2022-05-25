using Domain.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Libreria.Specifications
{
    public interface IEditorialService
    {
        Task<Categoria> GetEditorialAsync(int id, bool eager = false);
        Task<Categoria> GetEditorialAsync(Expression<Func<Categoria, bool>> cond, bool eager = false);
        Task<IEnumerable<Categoria>> GetAllEditorialesAsync(bool eager = false);
        Task<IEnumerable<Categoria>> GetEditorialesAsync(Expression<Func<Categoria, bool>> cond, bool eager = false);
        Task UpdateEditorialAsync(Categoria autor);
        Task DeleteEditorialAsync(int id);
        Task DeleteEditorialAsync(Expression<Func<Categoria, bool>> cond);
        Task<Categoria> CreateEditorialAsync(Categoria editorial);
        Task AgregarLibroAsync(int libroId, int editorialId);
        Task RemoverLibroAsync(int isbn, int editorialId);
    }
}
