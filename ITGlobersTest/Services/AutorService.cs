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
    public class AutorService
    {
        private readonly string apiUrl = "https://localhost:44315";
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<Producto> GetAutorAsync(int id)
        {
            var response = await httpClient.GetAsync($"{apiUrl}/api/Autores/{id}?eager=true");
            response.EnsureSuccessStatusCode();
            return (Producto)JsonConvert.DeserializeObject<ProductosDto>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> AsociarLibro(int libroId, int autorId)
        {
            var autor = await httpClient.GetAsync($"{apiUrl}/api/Autores/{autorId}?eager=false");
            await httpClient.PutAsync(
                $"{apiUrl}/api/Autores/{autorId}/Agregar-Libro/{libroId}",
                new StringContent(JsonConvert.SerializeObject(autor),
                Encoding.UTF8,
                "application/json"));
            return "Libro agregado";
        }

        public async Task<string> DesasociarLibro(int libroId, int autorId)
        {
            var autor = await httpClient.GetAsync($"{apiUrl}/api/Autores/{autorId}?eager=false");
            await httpClient.PutAsync(
                $"{apiUrl}/api/Autores/{autorId}/Remover-Libro/{libroId}",
                new StringContent(JsonConvert.SerializeObject(autor),
                Encoding.UTF8,
                "application/json"));
            return "Libro removido";
        }

        public async Task<IEnumerable<Producto>> GetAutoresAsync()
        {
            var response = httpClient.GetAsync($"{apiUrl}/api/Autores?eager=true").Result;
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<ProductosDto>>(await response.Content.ReadAsStringAsync()).Select(aDto => (Producto)aDto);
        }

        public async Task<Producto> SaveAutorAsync(Producto autor, int libroId)
        {
            var response = await httpClient.PostAsync($"{apiUrl}/api/Autores", autor, new JsonMediaTypeFormatter());
            response.EnsureSuccessStatusCode();
            var _autor = JsonConvert.DeserializeObject<ProductosDto>(await response.Content.ReadAsStringAsync());

            if (libroId != 0)
            {
                _ = await httpClient.PutAsync(
                    $"{apiUrl}/api/Autores/{_autor.Id}/Agregar-Libro/{libroId}",
                    new StringContent(JsonConvert.SerializeObject(autor),
                    Encoding.UTF8,
                    "application/json"));
            }
            return (Producto)JsonConvert.DeserializeObject<ProductosDto>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> DeleteAutorAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"{apiUrl}/api/Autores/{id}");
            return response.StatusCode != System.Net.HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : "";
        }

        public async Task UpdateAutorAsync(Producto autor)
        {
            var response = await httpClient.PutAsync(
                $"{apiUrl}/api/Autores",
                new StringContent(JsonConvert.SerializeObject(autor),
                Encoding.UTF8,
                "application/json"));
            response.EnsureSuccessStatusCode();
        }
    }
}
