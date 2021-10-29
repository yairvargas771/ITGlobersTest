using Domain.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Libreria.Specifications
{
    public interface IEditorialService
    {
        Task<Editorial> GetEditorialAsync(int id, bool eager = false);
        Task<Editorial> GetEditorialAsync(Expression<Func<Editorial, bool>> cond, bool eager = false);
        Task<IEnumerable<Editorial>> GetAllEditorialesAsync(bool eager = false);
        Task<IEnumerable<Editorial>> GetEditorialesAsync(Expression<Func<Editorial, bool>> cond, bool eager = false);
        Task UpdateEditorialAsync(Editorial autor);
        Task DeleteEditorialAsync(int id);
        Task DeleteEditorialAsync(Expression<Func<Editorial, bool>> cond);
        Task<Editorial> CreateEditorialAsync(Editorial editorial);
        Task AgregarLibroAsync(int libroId, int editorialId);
        Task RemoverLibroAsync(int isbn, int editorialId);
    }
}
