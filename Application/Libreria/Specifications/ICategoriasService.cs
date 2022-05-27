using Domain.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Libreria.Specifications
{
    public interface ICategoriasService
    {
        Task<Categoria> GetCategoriaAsync(int id, bool eager = false);
        Task<Categoria> GetCategoriaAsync(Expression<Func<Categoria, bool>> cond, bool eager = false);
        Task<IEnumerable<Categoria>> GetAllCategoriasAsync(bool eager = false);
        Task<IEnumerable<Categoria>> GetCategoriasAsync(Expression<Func<Categoria, bool>> cond, bool eager = false);
        Task UpdateCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(int id);
        Task DeleteCategoriaAsync(Expression<Func<Categoria, bool>> cond);
        Task<Categoria> CreateCategoriaAsync(Categoria categoria);
        Task AgregarProductoAsync(int idProducto, int idCategoria);
        Task RemoverProductoAsync(int idProducto, int idCategoria);
    }
}
