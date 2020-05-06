using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Api
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient(HttpClient client) => _client = client;

        public void AddHeader(string name, object value) =>
            _client.DefaultRequestHeaders.Add(name, Convert.ToString(value));

        public async Task<TResponse> DeleteAsync<TResponse>(string uri)
        {
            var response = await _client.DeleteAsync(uri);

            return JsonConvert.DeserializeObject<TResponse>(
                await response.Content.ReadAsStringAsync()
            );
        }

        public async Task<TResponse> GetAsync<TResponse>(string uri)
        {
            var response = await _client.GetAsync(uri);

            return JsonConvert.DeserializeObject<TResponse>(
                await response.Content.ReadAsStringAsync()
            );
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest value)
        {
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);

            return JsonConvert.DeserializeObject<TResponse>(
                await response.Content.ReadAsStringAsync()
            );
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string uri, TRequest value)
        {
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(uri, content);

            return JsonConvert.DeserializeObject<TResponse>(
                await response.Content.ReadAsStringAsync()
            );
        }

        public void AddAuthorizationHeader(string token) => _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", token);
    }
}
