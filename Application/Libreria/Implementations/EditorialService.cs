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
    public class EditorialService : IEditorialService
    {
        private readonly CategoriasUnitOfWork unitOfWork;

        public EditorialService(CategoriasUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AgregarLibroAsync(int isbn, int editorialId)
        {
            // Verificar si la editorial existe
            var editorial = await unitOfWork.CategoriaRepository.GetEntityAsync(editorialId, include: "Libros");
            if (editorial == null)
                throw new EntityNotFoundException(typeof(Categoria));

            // Guardar los cambios en el contexto
            CancellationToken cancelationToken = new CancellationToken();
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task<Categoria> CreateEditorialAsync(Categoria editorial)
        {
            CancellationToken cancelationToken = new CancellationToken();
            var result = await unitOfWork.CategoriaRepository.InsertEntityAsync(editorial);
            await unitOfWork.CommitAsync(cancelationToken);
            return result;
        }

        public async Task DeleteEditorialAsync(int id)
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

        public async Task DeleteEditorialAsync(Expression<Func<Categoria, bool>> cond)
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

        public async Task<IEnumerable<Categoria>> GetAllEditorialesAsync(bool eager = false)
        {
            return await unitOfWork.CategoriaRepository.GetEntitiesAsync(include: eager ? "Libros" : "");
        }

        public async Task<Categoria> GetEditorialAsync(int id, bool eager = false)
        {
            return await unitOfWork.CategoriaRepository.GetEntityAsync(id, eager ? "Libros": "");
        }

        public async Task<Categoria> GetEditorialAsync(Expression<Func<Categoria, bool>> cond, bool eager = false)
        {
            return await unitOfWork.CategoriaRepository.GetEntityAsync(cond, eager ? "Libros" : "");
        }

        public async Task<IEnumerable<Categoria>> GetEditorialesAsync(Expression<Func<Categoria, bool>> cond, bool eager = false)
        {
            return await unitOfWork.CategoriaRepository.GetEntitiesAsync(cond, eager ? "Libros" : "");
        }

        public async Task RemoverLibroAsync(int isbn, int editorialId)
        {
            // Verificar si la editorial existe
            var editorial = await unitOfWork.CategoriaRepository.GetEntityAsync(editorialId, include: "Libros");
            if (editorial == null)
                throw new EntityNotFoundException(typeof(Categoria));

            // Guardar los cambios
            CancellationToken cancellationToken = new CancellationToken();
            await unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task UpdateEditorialAsync(Categoria Editorial)
        {
            CancellationToken cancelationToken = new CancellationToken();
            unitOfWork.CategoriaRepository.UpdateEntity(Editorial);
            await unitOfWork.CommitAsync(cancelationToken);
        }
    }
}
