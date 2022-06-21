using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GatewayLogic
{
    public static class Common
    {
        public static async Task<HttpResponseMessage> SendApiRequest(IHttpClientFactory httpClientFactory, string requestUri)
        {
            var httpClient = httpClientFactory.CreateClient("ApiHttpClient");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(httpClient.BaseAddress.ToString() + requestUri),
                Headers =
                {
                    { "X-RapidAPI-Key", "cf0b762acbmsh4bdc9e4604c5ab5p1dc264jsne63230b32cc6" },
                    { "X-RapidAPI-Host", "api-formula-1.p.rapidapi.com" },
                }
            };

            return await httpClient.SendAsync(request);
        }
    }
}
