using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
using Infrastructure.Data;
using Infrastructure.Data.Libreria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Libreria.Implementations
{
    public class LibroService : ILibroService
    {
        private readonly LibreriaUnitOfWork unitOfWork;

        public LibroService(LibreriaUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Libro> CreateLibroAsync(Libro libro, int? autorId, int? editorialId)
        {
            CancellationToken cancelationToken = new CancellationToken();

            Autor autor;
            Editorial editorial;

            // Conseguir el autor
            if (autorId.HasValue)
            {
                autor = await unitOfWork.AutorRepository.GetEntityAsync(autorId.Value);

                // Verificar si el autor existe
                if (autor == null)
                    throw new EntityNotFoundException(typeof(Autor));

                //// Agregar el libro al autor
                //autor.Libros.Add(libro);
                libro.Autores = new List<Autor>();
                libro.Autores.Add(autor);
            }

            // Conseguir la editorial
            if (editorialId.HasValue)
            {
                editorial = await unitOfWork.EditorialRepository.GetEntityAsync(editorialId.Value);

                // Verificar si la editorial existe
                if (editorial == null)
                    throw new EntityNotFoundException(typeof(Editorial));

                //// Agregar el libro a la editorial
                //editorial.Libros.Add(libro);
                libro.Editorial = editorial;
            }

            // Verificar si el libro ya está registrado
            if (await unitOfWork.LibroRepository.EntityExistAsync(libro.Id))
                throw new EntityAlreadyExistException(typeof(Libro));

            // Insertar el libro
            var result = await unitOfWork.LibroRepository.InsertEntityAsync(libro);

            // Guardar los cambios
            await unitOfWork.CommitAsync(cancelationToken);

            return result;
        }

        public async Task DeleteLibroAsync(int id)
        {
            // Verificar si el libro existe
            if (!await unitOfWork.LibroRepository.EntityExistAsync(id))
            {
                throw new EntityNotFoundException(typeof(Libro));
            }

            // Eliminar el libro
            CancellationToken cancelationToken = new CancellationToken();
            await unitOfWork.LibroRepository.DeleteEntityAsync(id);
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task DeleteLibroAsync(Expression<Func<Libro, bool>> cond)
        {
            // Verificar si el libro existe
            var libro = unitOfWork.LibroRepository.GetEntityAsync(cond);

            if (libro == null)
            {
                throw new EntityNotFoundException(typeof(Libro));
            }

            // Eliminar el libro
            CancellationToken cancelationToken = new CancellationToken();
            await unitOfWork.LibroRepository.DeleteEntitiesAsync(cond);
            await unitOfWork.CommitAsync(cancelationToken);
        }

        public async Task<IEnumerable<Libro>> GetAllLibrosAsync(bool eager = false)
        {
            return await unitOfWork.LibroRepository.GetEntitiesAsync(null, eager ? "Autores,Editorial": "");
        }

        public async Task<Libro> GetLibroAsync(int id, bool eager = false)
        {
            return await unitOfWork.LibroRepository.GetEntityAsync(id, eager ? "Autores,Editorial" : "");
        }

        public async Task<Libro> GetLibroAsync(Expression<Func<Libro, bool>> cond, bool eager = false)
        {
            return await unitOfWork.LibroRepository.GetEntityAsync(cond, eager ? "Autores,Editorial" : "");
        }

        public async Task<IEnumerable<Libro>> GetLibrosAsync(Expression<Func<Libro, bool>> cond, bool eager = false)
        {
            return await unitOfWork.LibroRepository.GetEntitiesAsync(cond, eager ? "Autores,Editorial" : "");
        }

        public async Task UpdateLibroAsync(Libro Libro)
        {
            CancellationToken cancelationToken = new CancellationToken();
            unitOfWork.LibroRepository.UpdateEntity(Libro);
            await unitOfWork.CommitAsync(cancelationToken);
        }
    }
}
