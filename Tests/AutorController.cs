using Domain.Libreria;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Tests
{
    public class Tests
    {
        protected HttpClient TestClient;
        private bool Disposed;

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
            // Se crea el autor
            Autor autor = new Autor()
            {
                Nombre = "Jane",
                Apellidos = "Doe"
            };

            // Se registra el autor
            var respuesta = TestClient.PostAsync("/api/Autores", autor, new JsonMediaTypeFormatter()).Result;
            respuesta.EnsureSuccessStatusCode();
            var x = respuesta.Content.ReadAsStringAsync().Result;
            var respuestaCarga = System.Text.Json.JsonSerializer.Deserialize<Autor>(respuesta.Content.ReadAsStringAsync().Result);
            var idAutor = respuestaCarga.Id;

            // Se consulta el autor
            var c = TestClient.GetAsync($"api/Autores/{idAutor}").Result;
            c.EnsureSuccessStatusCode();
            var response = c.Content.ReadAsStringAsync().Result;
            var respuestaConsulta = System.Text.Json.JsonSerializer.Deserialize<Autor>(response);

            Assert.IsTrue(respuestaConsulta.Id == autor.Id);
        }
    }
}