using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
using Infrastructure.Data;
using Infrastructure.Data.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Libreria.Implementations
{
    public class ProductosService : IProductosService
    {
        private readonly CategoriasUnitOfWork unitOfWork;

        public ProductosService(CategoriasUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Producto> CreateProductoAsync(Producto producot)
        {
            CancellationToken cancelationToken = new CancellationToken();
            var result = await unitOfWork.ProductoRepository.InsertEntityAsync(producot);
            await unitOfWork.CommitAsync(cancelationToken);
            return result;
        }

        public async Task DeleteProductoAsync(int id)
        {
            // Verificar si el autor existe
            if (!await unitOfWork.ProductoRepository.EntityExistAsync(id))
            {
                throw new EntityNotFoundException(typeof(Producto));
            }

            // Eliminar el autor
            CancellationToken cancelationToken = new CancellationToken();
            await unitOfWork.ProductoRepository.DeleteEntityAsync(id);
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task DeleteProductoAsync(Expression<Func<Producto, bool>> cond)
        {
            // Verificar si la editorial existe
            var autor = unitOfWork.ProductoRepository.GetEntityAsync(cond);

            if (autor == null)
            {
                throw new EntityNotFoundException(typeof(Producto));
            }

            CancellationToken cancelationToken = new CancellationToken();
            await unitOfWork.ProductoRepository.DeleteEntitiesAsync(cond);
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task<IEnumerable<Producto>> GetAllProductosAsync(bool eager = false)
        {
            return await unitOfWork.ProductoRepository.GetEntitiesAsync(null, eager ? "Libros" : "");
        }

        public async Task<Producto> GetProductoAsync(int id, bool eager = false)
        {
            return await unitOfWork.ProductoRepository.GetEntityAsync(id, eager ? "Libros" : "");
        }

        public async Task<Producto> GetProductoAsync(Expression<Func<Producto, bool>> cond, bool eager = false)
        {
            return await unitOfWork.ProductoRepository.GetEntityAsync(cond, eager ? "Libros": "");
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync(Expression<Func<Producto, bool>> cond, bool eager = false)
        {
            return await unitOfWork.ProductoRepository.GetEntitiesAsync(cond, eager ? "Libros" : "");
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            CancellationToken cancelationToken = new CancellationToken();
            unitOfWork.ProductoRepository.UpdateEntity(producto);
            await unitOfWork.CommitAsync(cancelationToken);
        }
    }
}
