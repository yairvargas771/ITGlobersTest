using Domain.Libreria;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Categorias
{
    public class CategoriasContext : DbContext
    {
        private readonly IConfiguration Config;

        public DbSet<Producto> Autores;
        public DbSet<Categoria> Editoriales;

        public CategoriasContext(DbContextOptions<CategoriasContext> options, IConfiguration config) : base(options)
        {
            Config = config;
        }

        public async Task CommitAsync(CancellationToken cancelationToken)
        {
            await SaveChangesAsync(cancelationToken).ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Config.GetValue<string>("SchemaName"));

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>();
            modelBuilder.Entity<Categoria>();
        }
    }
}
