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
    public class EditorialService
    {
        private string apiUrl = "https://localhost:44315";
        private HttpClient httpClient = new HttpClient();

        public async Task<Editorial> GetEditorialAsync(int id)
        {
            var response = await httpClient.GetAsync($"{apiUrl}/api/Editoriales/{id}?eager=true");
            response.EnsureSuccessStatusCode();
            return (Editorial)JsonConvert.DeserializeObject<EditorialDto>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<Editorial>> GetEditorialesAsync()
        {
            var response = httpClient.GetAsync($"{apiUrl}/api/Editoriales?eager=true").Result;
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<EditorialDto>>(await response.Content.ReadAsStringAsync()).Select(aDto => (Editorial)aDto);
        }

        public async Task<Editorial> SaveEditorialAsync(Editorial editorial, int libroId)
        {
            var response = await httpClient.PostAsync($"{apiUrl}/api/Editoriales", editorial, new JsonMediaTypeFormatter());
            response.EnsureSuccessStatusCode();
            var _editorial = JsonConvert.DeserializeObject<EditorialDto>(await response.Content.ReadAsStringAsync());

            if (libroId != 0)
            {
                _ = await httpClient.PutAsync(
                    $"{apiUrl}/api/Editoriales/{_editorial.Id}/Agregar-Libro/{libroId}",
                    new StringContent(JsonConvert.SerializeObject(editorial),
                    Encoding.UTF8,
                    "application/json"));
            }
            return (Editorial)JsonConvert.DeserializeObject<EditorialDto>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> DeleteEditorialAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"{apiUrl}/api/Editoriales/{id}");
            return response.StatusCode != System.Net.HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : "";
        }

        public async Task UpdateEditorialAsync(Editorial editorial)
        {
            var response = await httpClient.PutAsync(
                $"{apiUrl}/api/Editoriales",
                new StringContent(JsonConvert.SerializeObject(editorial),
                Encoding.UTF8,
                "application/json"));
            response.EnsureSuccessStatusCode();
        }
    }
}
