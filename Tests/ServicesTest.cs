using Application.Libreria.Specifications;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class ServicesTest
    {
        private IAutorService autorService;
        private ILibroService libroService;
        private IEditorialService editorialService;

        [SetUp]
        public void Setup()
        {
            var appFactory = new WebApplicationFactory<WebApi.Startup>();
            var scope = appFactory.Services.CreateScope();
            autorService = (IAutorService)scope.ServiceProvider.GetRequiredService(typeof(IAutorService));
            libroService = (ILibroService)scope.ServiceProvider.GetRequiredService(typeof(ILibroService));
            editorialService = (IEditorialService)scope.ServiceProvider.GetRequiredService(typeof(IEditorialService));
        }

        [Test]
        public void VerificarQueUnLibroRecienCreadoPuedeAsociarseConUnAutorYUnaEditorialExistentes()
        {
            // Crear un autor
            var autor = autorService.CreateAutorAsync(new Domain.Libreria.Autor() {
                Apellidos = "Vargas Delgado",
                Nombre = "Yair Alfredo"
            }).Result;

            // Crear una editorial
            var editorial = editorialService.CreateEditorialAsync(new Domain.Libreria.Editorial()
            {
                Nombre = "Editorial de prueba",
                Sede = "Sede de prueba"
            }).Result;

            // Crear un libro
            var libro = libroService.CreateLibroAsync(new Domain.Libreria.Libro()
            {
                Paginas = "100",
                Sinopsis = "Sinopsis de prueba",
                Titulo = "Titulo de prueba"
            }, autor.Id, editorial.Id).Result;

            // Obtiene el libro
            libro = libroService.GetLibrosAsync(libro => libro.Id == libro.Id, true).Result.FirstOrDefault();

            // Verifica si la relación con el autor y la editorial se hizo correctamente
            Assert.IsTrue(libro.Autores.Contains(autor));
            Assert.IsTrue(libro.Editorial.Id == editorial.Id);
        }
    }
}
