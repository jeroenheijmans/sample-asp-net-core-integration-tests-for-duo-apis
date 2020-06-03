using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleDuoApis.FooApi
{
    public static class HttpClientExtensions
    {
        public static async Task<T> Get<T>(this HttpClient httpClient, string uri)
        {
            var response = await httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode) 
            {
                throw new Exception($"Http Error {response.StatusCode} with message: {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(json);

            return result;
        }
    }
}