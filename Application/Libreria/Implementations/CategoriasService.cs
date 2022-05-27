using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
using Infrastructure.Data;
using Infrastructure.Data.Categorias;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Libreria.Implementations
{
    public class CategoriasService : ICategoriasService
    {
        private readonly CategoriasUnitOfWork unitOfWork;

        public CategoriasService(CategoriasUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AgregarProductoAsync(int idProducto, int idCategoria)
        {
            // Verificar si la editorial existe
            var categoria = await unitOfWork.CategoriaRepository.GetEntityAsync(idCategoria, include: "Libros");
            if (categoria == null)
                throw new EntityNotFoundException(typeof(Categoria));

            // Guardar los cambios en el contexto
            CancellationToken cancelationToken = new CancellationToken();
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task<Categoria> CreateCategoriaAsync(Categoria editorial)
        {
            CancellationToken cancelationToken = new CancellationToken();
            var result = await unitOfWork.CategoriaRepository.InsertEntityAsync(editorial);
            await unitOfWork.CommitAsync(cancelationToken);
            return result;
        }

        public async Task DeleteCategoriaAsync(int id)
        {
            // Verificar si la editorial existe
            if (!await unitOfWork.CategoriaRepository.EntityExistAsync(id))
            {
                throw new EntityNotFoundException(typeof(Categoria));
            }

            try
            {
                CancellationToken cancelationToken = new CancellationToken();
                await unitOfWork.CategoriaRepository.DeleteEntityAsync(id);
                await unitOfWork.CommitAsync(cancelationToken);
            }
            catch (DbUpdateException e)
            {
                switch (((SqlException)e.InnerException).Number)
                {
                    case 547:
                        throw new ReferenceConstrainViolationException(typeof(Categoria), typeof(Producto));
                    default:
                        throw;
                }
            }
        }

        public async Task DeleteCategoriaAsync(Expression<Func<Categoria, bool>> cond)
        {
            // Verificar si la editorial existe
            var editorial = unitOfWork.CategoriaRepository.GetEntityAsync(cond);

            if (editorial == null)
            {
                throw new EntityNotFoundException(typeof(Categoria));
            }

            try
            {
                CancellationToken cancelationToken = new CancellationToken();
                await unitOfWork.CategoriaRepository.DeleteEntitiesAsync(cond);
                await unitOfWork.CommitAsync(cancelationToken);
            }
            catch (DbUpdateException e)
            {
                switch (((SqlException)e.InnerException).Number)
                {
                    case 547:
                        throw new ReferenceConstrainViolationException(typeof(Categoria), typeof(Producto));
                    default:
                        throw;
                }
            }
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync(bool eager = false)
        {
            return await unitOfWork.CategoriaRepository.GetEntitiesAsync(include: eager ? "Libros" : "");
        }

        public async Task<Categoria> GetCategoriaAsync(int id, bool eager = false)
        {
            return await unitOfWork.CategoriaRepository.GetEntityAsync(id, eager ? "Libros": "");
        }

        public async Task<Categoria> GetCategoriaAsync(Expression<Func<Categoria, bool>> cond, bool eager = false)
        {
            return await unitOfWork.CategoriaRepository.GetEntityAsync(cond, eager ? "Libros" : "");
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync(Expression<Func<Categoria, bool>> cond, bool eager = false)
        {
            return await unitOfWork.CategoriaRepository.GetEntitiesAsync(cond, eager ? "Libros" : "");
        }

        public async Task RemoverProductoAsync(int idProducto, int idCategoria)
        {
            // Verificar si la editorial existe
            var categoria = await unitOfWork.CategoriaRepository.GetEntityAsync(idCategoria, include: "Libros");
            if (categoria == null)
                throw new EntityNotFoundException(typeof(Categoria));

            // Guardar los cambios
            CancellationToken cancellationToken = new CancellationToken();
            await unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task UpdateCategoriaAsync(Categoria categoria)
        {
            CancellationToken cancelationToken = new CancellationToken();
            unitOfWork.CategoriaRepository.UpdateEntity(categoria);
            await unitOfWork.CommitAsync(cancelationToken);
        }
    }
}
