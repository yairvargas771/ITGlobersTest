using Domain.Libreria;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Libreria
{
    public class LibreriaContext : DbContext
    {
        private readonly IConfiguration Config;

        public DbSet<Autor> Autores;
        public DbSet<Editorial> Editoriales;
        public DbSet<Libro> Libros;

        public LibreriaContext(DbContextOptions<LibreriaContext> options, IConfiguration config) : base(options)
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
            modelBuilder.Entity<Autor>();
            modelBuilder.Entity<Editorial>();
            modelBuilder.Entity<Libro>();
        }
    }
}
