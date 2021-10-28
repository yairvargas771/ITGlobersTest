using Domain.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Libreria.Specifications
{
    public interface ILibroService
    {
        Task<Libro> GetLibroAsync(int id);
        Task<Libro> GetLibroAsync(Expression<Func<Libro, bool>> cond);
        Task<IEnumerable<Libro>> GetAllLibrosAsync();
        Task<IEnumerable<Libro>> GetLibrosAsync(Expression<Func<Libro, bool>> cond);
        Task UpdateLibroAsync(Libro autor);
        Task DeleteLibroAsync(int id);
        Task DeleteLibroAsync(Expression<Func<Libro, bool>> cond);
        Task<Libro> CreateLibroAsync(Libro libro, int? autorId, int? editorialId);
    }
}
