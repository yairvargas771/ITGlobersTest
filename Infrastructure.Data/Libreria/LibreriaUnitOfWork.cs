using Infrastructure.Data.Libreria.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Libreria
{
    public class LibreriaUnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly LibreriaContext LibreriaContext;

        private LibroRepository libroRepository;
        public LibroRepository LibroRepository => libroRepository ?? new LibroRepository(LibreriaContext);

        private AutorRepository autorRepository;
        public AutorRepository AutorRepository => autorRepository ?? new AutorRepository(LibreriaContext);

        private EditorialRepository editorialRepository;
        public EditorialRepository EditorialRepository => editorialRepository ?? new EditorialRepository(LibreriaContext);

        public LibreriaUnitOfWork(LibreriaContext libreriaContext)
        {
            LibreriaContext = libreriaContext;
        }

        public Task CommitAsync(CancellationToken cancelationToken)
        {
            return LibreriaContext.CommitAsync(cancelationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                libroRepository = null;
                autorRepository = null;
                editorialRepository = null;
                LibreriaContext.Dispose();
            }

            _disposed = true;
        }
    }
}
