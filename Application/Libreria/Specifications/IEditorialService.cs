using Domain.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Libreria.Specifications
{
    public interface IEditorialService
    {
        Task<Editorial> GetEditorialAsync(int id);
        Task<Editorial> GetEditorialAsync(Expression<Func<Editorial, bool>> cond);
        Task<IEnumerable<Editorial>> GetAllEditorialesAsync();
        Task<IEnumerable<Editorial>> GetEditorialesAsync(Expression<Func<Editorial, bool>> cond);
        Task UpdateEditorialAsync(Editorial autor);
        Task DeleteEditorialAsync(int id);
        Task DeleteEditorialAsync(Expression<Func<Editorial, bool>> cond);
        Task<Editorial> CreateEditorialAsync(Editorial editorial);
        Task AgregarLibroAsync(int libroId, int editorialId);
        Task RemoverLibroAsync(int isbn, int editorialId);
    }
}
