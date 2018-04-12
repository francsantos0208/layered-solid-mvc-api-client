using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Client
{
    public class HttpClientSingleton : HttpClient
    {
        private static readonly HttpClientSingleton HttpClientSingletonInstance;

        static HttpClientSingleton()
        {
            HttpClientSingletonInstance = new HttpClientSingleton();
        }

        private HttpClientSingleton()
        {
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Timeout = TimeSpan.FromMilliseconds(15000);
            MaxResponseContentBufferSize = 256000;
        }

        public static HttpClientSingleton Instance => HttpClientSingletonInstance;

        public async Task<T> GetData<T>(string endPoint)
        {
            var uri = new Uri(endPoint);

            var response = await GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(content);
                return data;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}