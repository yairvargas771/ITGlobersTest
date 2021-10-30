using Domain.Libreria;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace ITGlobersTest.Services
{
    public class LibroService
    {
        private string apiUrl = "https://localhost:44315";
        private HttpClient httpClient = new HttpClient();

        public async Task<Libro> GetLibroAsync(int id)
        {
            var response = await httpClient.GetAsync($"{apiUrl}/api/Libros/{id}?eager=true");
            response.EnsureSuccessStatusCode();
            return (Libro)JsonConvert.DeserializeObject<LibroDto>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<Libro>> GetLibrosAsync()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/api/Libros?eager=true");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<LibroDto>>(await response.Content.ReadAsStringAsync()).Select(aDto => (Libro)aDto);
        }

        public async Task<Libro> SaveLibroAsync(Libro libro, int idAutor, int idEditorial)
        {
            var response = await httpClient.PostAsync($"{apiUrl}/api/Libros?eager=true" + (idAutor != 0 ? "&autorId=" + idAutor : "") + (idEditorial != 0 ? "&editorialId=" + idEditorial : ""), libro, new JsonMediaTypeFormatter());
            response.EnsureSuccessStatusCode();
            return (Libro)JsonConvert.DeserializeObject<LibroDto>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> DeleteLibroAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"{apiUrl}/api/Libros/{id}");
            return response.StatusCode != System.Net.HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : "";
        }

        public async Task UpdateLibroAsync(Libro libro)
        {
            var response = await httpClient.PutAsync(
                $"{apiUrl}/api/Libros",
                new StringContent(JsonConvert.SerializeObject(libro),
                Encoding.UTF8,
                "application/json"));
            response.EnsureSuccessStatusCode();
        }
    }
}
