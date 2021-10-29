using Domain.Libreria;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;

namespace Tests
{
    public class Tests
    {
        protected HttpClient TestClient;
        private bool Disposed;

        #region
        private LibroDto registrarLibro()
        {
            // Se crea el libro
            Libro libro = new Libro()
            {
                Paginas = "100",
                Titulo = "Test Libro",
                Sinopsis = "Test Libro"
            };

            // Se registra el libro
            var libroCreadoResponse = TestClient.PostAsync("/api/Libros", libro, new JsonMediaTypeFormatter()).Result;
            libroCreadoResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<LibroDto>(libroCreadoResponse.Content.ReadAsStringAsync().Result);
        }

        private LibroDto obtenerLibro(int isbn)
        {
            var libroDto = TestClient.GetAsync($"/api/Libros/{isbn}?eager=true").Result;
            return JsonConvert.DeserializeObject<LibroDto>(libroDto.Content.ReadAsStringAsync().Result);
        }

        private void eliminarLibro(int id)
        {
            _ = TestClient.DeleteAsync($"/api/Libros/{id}").Result;
        }

        private AutorDto registrarAutor()
        {
            // Se crea el autor
            Autor autor = new Autor()
            {
                Nombre = "Jane",
                Apellidos = "Doe"
            };

            // Se registra el autor
            var creacionDeAutorResponse = TestClient.PostAsync("/api/Autores", autor, new JsonMediaTypeFormatter()).Result;
            creacionDeAutorResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<AutorDto>(creacionDeAutorResponse.Content.ReadAsStringAsync().Result);
        }

        private void eliminarAutor(int id)
        {
            _ = TestClient.DeleteAsync($"/api/Autores/{id}").Result;
        }

        private AutorDto obtenerAutor(int id)
        {
            var autorDto = TestClient.GetAsync($"/api/Autores/{id}?eager=true").Result;
            return JsonConvert.DeserializeObject<AutorDto>(autorDto.Content.ReadAsStringAsync().Result);
        }

        private EditorialDto registrarEditorial()
        {
            // Se crea la editorial
            Editorial editorial = new Editorial()
            {
                Nombre = "Editorial Test",
                Sede = "Sede Test"
            };

            // Se registra la editorial
            var creacionDeEditorialResponse = TestClient.PostAsync("/api/Editoriales", editorial, new JsonMediaTypeFormatter()).Result;
            creacionDeEditorialResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<EditorialDto>(creacionDeEditorialResponse.Content.ReadAsStringAsync().Result);
        }

        private EditorialDto obtenerEditorial(int id)
        {
            var editorialDto = TestClient.GetAsync($"/api/Editoriales/{id}?eager=true").Result;
            return JsonConvert.DeserializeObject<EditorialDto>(editorialDto.Content.ReadAsStringAsync().Result);
        }

        private void eliminarEditorial(int id)
        {
            _ = TestClient.DeleteAsync($"/api/Editoriales/{id}").Result;
        }
        #endregion

        [SetUp]
        public void Setup()
        {
            Disposed = false;
            var appFactory = new WebApplicationFactory<WebApi.Startup>();
            TestClient = appFactory.CreateClient();
        }

        [Test]
        public void CrearAutorTest()
        {
            // Se registra el autor
            var autorRegistrado = registrarAutor();

            // Se consulta el autor
            var autorObtenido = obtenerAutor(autorRegistrado.Id);

            // Verificar que el Id de autor creado es el mismo que el del autor consultado
            Assert.IsTrue(autorRegistrado.Id == autorObtenido.Id);

            // Se elimina el autor
            eliminarAutor(autorObtenido.Id);
        }

        [Test]
        public void EliminarAutorTest()
        {
            // Se registra el autor
            var autorRegistrado = registrarAutor();

            // Se verifica que el autor se creo correctamente
            var autorObtenido = obtenerAutor(autorRegistrado.Id);

            // Se elimina el autor
            eliminarAutor(autorObtenido.Id);

            // Se verifica que el autor no existe
            autorObtenido = obtenerAutor(autorRegistrado.Id);

            // Verificar que el Id de autor creado es el mismo que el del autor consultado
            Assert.IsTrue(autorObtenido.Id == 0);
        }

        [Test]
        public void AgregarYEliminarLibroAAutorTest()
        {
            // Se registra el libro
            var libro = registrarLibro();

            // Se registra el autor
            var autor = registrarAutor();

            // Se agrega el libro al autor recien creado
            var autorDto = TestClient.PutAsync(
                $"/api/Autores/{autor.Id}/Agregar-Libro/{libro.Id}", 
                new StringContent(JsonConvert.SerializeObject(autor), 
                Encoding.UTF8, 
                "application/json")).Result;
            autorDto.EnsureSuccessStatusCode();

            // Se obtiene el autor con el libro agregado
            autor = obtenerAutor(autor.Id);

            // Se verifica que el Id de autor creado es el mismo que el del autor consultado
            Assert.IsTrue(autor.Libros.Where(libro => libro.Id == libro.Id).Any());

            // Se desvincula el libro a del autor
            autorDto = TestClient.PutAsync(
                $"/api/Autores/{autor.Id}/Remover-Libro/{libro.Id}",
                new StringContent(JsonConvert.SerializeObject(autor),
                Encoding.UTF8,
                "application/json")).Result;
            autorDto.EnsureSuccessStatusCode();

            // Se obtiene el autor con el libro eliminado
            autor = obtenerAutor(autor.Id);

            // Se verifica que el autor no tiene el libro
            Assert.IsTrue(!autor.Libros.Where(libro => libro.Id == libro.Id).Any());

            // Limpiar db
            eliminarLibro(libro.Id);
            eliminarAutor(autor.Id);
        }

        [Test]
        public void AgregarYEliminarLibroEnEditorial()
        {
            var libro = registrarLibro();
            var editorial = registrarEditorial();

            // Se agrega el libro a la editorial recien creada
            var editorialDto = TestClient.PutAsync(
                $"/api/Editoriales/{editorial.Id}/Agregar-Libro/{libro.Id}",
                new StringContent(JsonConvert.SerializeObject(editorial),
                Encoding.UTF8,
                "application/json")).Result;
            editorialDto.EnsureSuccessStatusCode();

            // Se obtiene la editorial con el libro agregado
            editorial = obtenerEditorial(editorial.Id);

            // Se verifica que la editorial tiene un libro con el id del libro creado
            Assert.IsTrue(editorial.Libros.Where(libro => libro.Id == libro.Id).Any());

            // Se desvincula el libro a la editorial
            editorialDto = TestClient.PutAsync(
                $"/api/Editoriales/{editorial.Id}/Remover-Libro/{libro.Id}",
                new StringContent(JsonConvert.SerializeObject(editorial),
                Encoding.UTF8, 
                "application/json")).Result;
            editorialDto.EnsureSuccessStatusCode();

            // Se obtiene la editorial con el libro eliminado
            editorial = obtenerEditorial(editorial.Id);

            // Se verifica que la editorial tiene no tiene el libro
            Assert.IsTrue(!editorial.Libros.Where(libro => libro.Id == libro.Id).Any());

            // Limpiar db
            eliminarLibro(libro.Id);
            eliminarEditorial(editorial.Id);
        }
    }
}