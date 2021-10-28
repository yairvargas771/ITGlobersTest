using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
using Infrastructure.Data;
using Infrastructure.Data.Libreria;
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
            // Verificar si el autor existe
            var autor = await unitOfWork.AutorRepository.GetEntityAsync(editorialId);
            if (autor != null)
                throw new EntityNotFoundException(typeof(Autor));

            // Verificar si el libro existe
            var libro = await unitOfWork.LibroRepository.GetEntityAsync(isbn);
            if (libro == null)
                throw new EntityNotFoundException(typeof(Libro));

            // Verificar si el autor ya está relacionado con este libro
            if (autor.Libros.Contains(libro))
                throw new EntityIsAlreadyRelatedToException(typeof(Autor), typeof(Libro));

            // Relacionar el libro al autor
            autor.Libros.Add(libro);
            unitOfWork.AutorRepository.UpdateEntity(autor);

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
            CancellationToken cancelationToken = new CancellationToken();
            await unitOfWork.EditorialRepository.DeleteEntityAsync(id);
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task DeleteEditorialAsync(Expression<Func<Editorial, bool>> cond)
        {
            CancellationToken cancelationToken = new CancellationToken();
            await unitOfWork.EditorialRepository.DeleteEntitiesAsync(cond);
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task<IEnumerable<Editorial>> GetAllEditorialesAsync()
        {
            return await unitOfWork.EditorialRepository.GetEntitiesAsync();
        }

        public async Task<Editorial> GetEditorialAsync(int id)
        {
            return await unitOfWork.EditorialRepository.GetEntityAsync(id);
        }

        public async Task<Editorial> GetEditorialAsync(Expression<Func<Editorial, bool>> cond)
        {
            return await unitOfWork.EditorialRepository.GetEntityAsync(cond);
        }

        public async Task<IEnumerable<Editorial>> GetEditorialesAsync(Expression<Func<Editorial, bool>> cond)
        {
            return await unitOfWork.EditorialRepository.GetEntitiesAsync(cond);
        }

        public async Task RemoverLibroAsync(int isbn, int editorialId)
        {
            // Verificar si la editorial existe
            var autor = await unitOfWork.EditorialRepository.GetEntityAsync(editorialId);
            if (autor == null)
                throw new EntityNotFoundException(typeof(Editorial));

            // Verificar si el libro existe
            var libro = await unitOfWork.LibroRepository.GetEntityAsync(isbn);
            if (libro == null)
                throw new EntityNotFoundException(typeof(Libro));

            // Verificar que la editorial esté relacionado con el libro
            if (autor.Libros.Where(libro => libro.Id == isbn).Any())
                throw new EntityIsNotRelatedToException(typeof(Editorial), typeof(Libro));

            // Remover el libro del autor
            autor.Libros.Remove(libro);

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
