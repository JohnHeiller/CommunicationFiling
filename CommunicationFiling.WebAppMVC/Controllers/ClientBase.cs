using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationFiling.WebAppClient.Controllers
{
    public sealed class ClientBase<T> : IDisposable
    {
        static HttpClient Client;

        public ClientBase(string servicesConnection)
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(servicesConnection) //"https://localhost:44363/"
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<long> CreateData(string path, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PostAsync(path, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var dataResult = JsonConvert.DeserializeObject<long>(jsonResult);

                return dataResult;
            }

            return 0;
        }

        public async Task<T> GetTAsync(string path)
        {
            HttpResponseMessage response = await Client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(json);

                return data;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<long> UpdateTAsync(string path, T data, long id)
        {
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PutAsync(path + id, stringContent);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return id;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<HttpStatusCode> DeleteTAsync(string path, long id)
        {
            var json = JsonConvert.SerializeObject("");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PostAsync(path + id.ToString() + "/", stringContent);

            return response.StatusCode;
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
