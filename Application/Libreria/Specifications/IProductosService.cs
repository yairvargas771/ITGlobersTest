using Domain.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Libreria.Specifications
{
    public interface IProductosService
    {
        Task<Producto> GetProductoAsync(int id, bool eager = false);
        Task<Producto> GetProductoAsync(Expression<Func<Producto, bool>> cond, bool eager = false);
        Task<IEnumerable<Producto>> GetAllProductosAsync(bool eager = false);
        Task<IEnumerable<Producto>> GetProductosAsync(Expression<Func<Producto, bool>> cond, bool eager = false);
        Task UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
        Task DeleteProductoAsync(Expression<Func<Producto, bool>> cond);
        Task<Producto> CreateProductoAsync(Producto producto);
    }
}
