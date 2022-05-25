using Infrastructure.Data.Categorias.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Categorias
{
    public class CategoriasUnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly CategoriasContext LibreriaContext;

        private ProductoRepository productoRepository;
        public ProductoRepository ProductoRepository => productoRepository ?? new ProductoRepository(LibreriaContext);

        private CategoriaRepository categoriaRepository;
        public CategoriaRepository CategoriaRepository => categoriaRepository ?? new CategoriaRepository(LibreriaContext);

        public CategoriasUnitOfWork(CategoriasContext CategoriasContext)
        {
            LibreriaContext = CategoriasContext;
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
                productoRepository = null;
                categoriaRepository = null;
                LibreriaContext.Dispose();
            }

            _disposed = true;
        }
    }
}
