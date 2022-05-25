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
    public class AutorService : IAutorService
    {
        private readonly CategoriasUnitOfWork unitOfWork;

        public AutorService(CategoriasUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AgregarLibroAsync(int isbn, int autorId)
        {
            CancellationToken cancelationToken = new CancellationToken();

            // Verificar si el autor existe
            var autor = await unitOfWork.ProductoRepository.GetEntityAsync(autorId, include: "Libros");
            if (autor == null)
                throw new EntityNotFoundException(typeof(Producto));

            // Guardar los cambios en el contexto
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task<Producto> CreateAutorAsync(Producto autor)
        {
            CancellationToken cancelationToken = new CancellationToken();
            var result = await unitOfWork.ProductoRepository.InsertEntityAsync(autor);
            await unitOfWork.CommitAsync(cancelationToken);
            return result;
        }

        public async Task DeleteAutorAsync(int id)
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

        public async Task DeleteAutorAsync(Expression<Func<Producto, bool>> cond)
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

        public async Task<IEnumerable<Producto>> GetAllAutoresAsync(bool eager = false)
        {
            return await unitOfWork.ProductoRepository.GetEntitiesAsync(null, eager ? "Libros" : "");
        }

        public async Task<Producto> GetAutorAsync(int id, bool eager = false)
        {
            return await unitOfWork.ProductoRepository.GetEntityAsync(id, eager ? "Libros" : "");
        }

        public async Task<Producto> GetAutorAsync(Expression<Func<Producto, bool>> cond, bool eager = false)
        {
            return await unitOfWork.ProductoRepository.GetEntityAsync(cond, eager ? "Libros": "");
        }

        public async Task<IEnumerable<Producto>> GetAutoresAsync(Expression<Func<Producto, bool>> cond, bool eager = false)
        {
            return await unitOfWork.ProductoRepository.GetEntitiesAsync(cond, eager ? "Libros" : "");
        }

        public async Task RemoverLibroAsync(int isbn, int autorId)
        {
            // Verificar si el autor existe
            var autor = await unitOfWork.ProductoRepository.GetEntityAsync(autorId, include: "Libros");
            if (autor == null)
                throw new EntityNotFoundException(typeof(Producto));

            // Guardar los cambios
            CancellationToken cancellationToken = new CancellationToken();
            await unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task UpdateAutorAsync(Producto autor)
        {
            CancellationToken cancelationToken = new CancellationToken();
            unitOfWork.ProductoRepository.UpdateEntity(autor);
            await unitOfWork.CommitAsync(cancelationToken);
        }
    }
}
