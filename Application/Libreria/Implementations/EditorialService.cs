using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
using Infrastructure.Data;
using Infrastructure.Data.Libreria;
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
        private readonly LibreriaUnitOfWork unitOfWork;

        public EditorialService(LibreriaUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AgregarLibroAsync(int isbn, int editorialId)
        {
            // Verificar si la editorial existe
            var editorial = await unitOfWork.EditorialRepository.GetEntityAsync(editorialId, include: "Libros");
            if (editorial == null)
                throw new EntityNotFoundException(typeof(Editorial));

            // Verificar si el libro existe
            var libro = await unitOfWork.LibroRepository.GetEntityAsync(isbn);
            if (libro == null)
                throw new EntityNotFoundException(typeof(Libro));

            // Verificar si la editorial ya está relacionado con este libro
            if (editorial.Libros.Contains(libro))
                throw new EntityIsAlreadyRelatedToException(typeof(Editorial), typeof(Libro));

            // Relacionar el libro con la editorial
            editorial.Libros.Add(libro);
            unitOfWork.EditorialRepository.UpdateEntity(editorial);

            // Guardar los cambios en el contexto
            CancellationToken cancelationToken = new CancellationToken();
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task<Editorial> CreateEditorialAsync(Editorial editorial)
        {
            CancellationToken cancelationToken = new CancellationToken();
            var result = await unitOfWork.EditorialRepository.InsertEntityAsync(editorial);
            await unitOfWork.CommitAsync(cancelationToken);
            return result;
        }

        public async Task DeleteEditorialAsync(int id)
        {
            // Verificar si la editorial existe
            if (!await unitOfWork.EditorialRepository.EntityExistAsync(id))
            {
                throw new EntityNotFoundException(typeof(Editorial));
            }

            try
            {
                CancellationToken cancelationToken = new CancellationToken();
                await unitOfWork.EditorialRepository.DeleteEntityAsync(id);
                await unitOfWork.CommitAsync(cancelationToken);
            }
            catch (DbUpdateException e)
            {
                switch (((SqlException)e.InnerException).Number)
                {
                    case 547:
                        throw new ReferenceConstrainViolationException(typeof(Editorial), typeof(Libro));
                    default:
                        throw;
                }
            }
        }

        public async Task DeleteEditorialAsync(Expression<Func<Editorial, bool>> cond)
        {
            // Verificar si la editorial existe
            var editorial = unitOfWork.EditorialRepository.GetEntityAsync(cond);

            if (editorial == null)
            {
                throw new EntityNotFoundException(typeof(Editorial));
            }

            try
            {
                CancellationToken cancelationToken = new CancellationToken();
                await unitOfWork.EditorialRepository.DeleteEntitiesAsync(cond);
                await unitOfWork.CommitAsync(cancelationToken);
            }
            catch (DbUpdateException e)
            {
                switch (((SqlException)e.InnerException).Number)
                {
                    case 547:
                        throw new ReferenceConstrainViolationException(typeof(Editorial), typeof(Libro));
                    default:
                        throw;
                }
            }
        }

        public async Task<IEnumerable<Editorial>> GetAllEditorialesAsync(bool eager = false)
        {
            return await unitOfWork.EditorialRepository.GetEntitiesAsync(include: eager ? "Libros" : "");
        }

        public async Task<Editorial> GetEditorialAsync(int id, bool eager = false)
        {
            return await unitOfWork.EditorialRepository.GetEntityAsync(id, eager ? "Libros": "");
        }

        public async Task<Editorial> GetEditorialAsync(Expression<Func<Editorial, bool>> cond, bool eager = false)
        {
            return await unitOfWork.EditorialRepository.GetEntityAsync(cond, eager ? "Libros" : "");
        }

        public async Task<IEnumerable<Editorial>> GetEditorialesAsync(Expression<Func<Editorial, bool>> cond, bool eager = false)
        {
            return await unitOfWork.EditorialRepository.GetEntitiesAsync(cond, eager ? "Libros" : "");
        }

        public async Task RemoverLibroAsync(int isbn, int editorialId)
        {
            // Verificar si la editorial existe
            var editorial = await unitOfWork.EditorialRepository.GetEntityAsync(editorialId, include: "Libros");
            if (editorial == null)
                throw new EntityNotFoundException(typeof(Editorial));

            // Verificar si el libro existe
            var libro = await unitOfWork.LibroRepository.GetEntityAsync(isbn);
            if (libro == null)
                throw new EntityNotFoundException(typeof(Libro));

            // Verificar que la editorial esté relacionado con el libro
            if (!editorial.Libros.Where(libro => libro.Id == isbn).Any())
                throw new EntityIsNotRelatedToException(typeof(Editorial), typeof(Libro));

            // Remover el libro de la editorial
            editorial.Libros.Remove(libro);
            unitOfWork.EditorialRepository.UpdateEntity(editorial);

            // Guardar los cambios
            CancellationToken cancellationToken = new CancellationToken();
            await unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task UpdateEditorialAsync(Editorial Editorial)
        {
            CancellationToken cancelationToken = new CancellationToken();
            unitOfWork.EditorialRepository.UpdateEntity(Editorial);
            await unitOfWork.CommitAsync(cancelationToken);
        }
    }
}
