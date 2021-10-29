using Domain.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Libreria.Specifications
{
    public interface ILibroService
    {
        Task<Libro> GetLibroAsync(int id, bool eager = false);
        Task<Libro> GetLibroAsync(Expression<Func<Libro, bool>> cond, bool eager = false);
        Task<IEnumerable<Libro>> GetAllLibrosAsync(bool eager = false);
        Task<IEnumerable<Libro>> GetLibrosAsync(Expression<Func<Libro, bool>> cond, bool eager = false);
        Task UpdateLibroAsync(Libro autor);
        Task DeleteLibroAsync(int id);
        Task DeleteLibroAsync(Expression<Func<Libro, bool>> cond);
        Task<Libro> CreateLibroAsync(Libro libro, int? autorId, int? editorialId);
    }
}
