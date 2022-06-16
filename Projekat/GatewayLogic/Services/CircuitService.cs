using System;
using System.Net.Http;
using System.Threading.Tasks;
using GatewayLogic.Interfaces;

namespace GatewayLogic.Services
{
    public class CircuitService : ICircuitService
    {
        private HttpClient _HttpClient;

        public CircuitService(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public async Task<string> GetCircuits()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api-nba-v1.p.rapidapi.com/teams"),
                Headers =
                {
                    { "X-RapidAPI-Key", "cf0b762acbmsh4bdc9e4604c5ab5p1dc264jsne63230b32cc6" },
                    { "X-RapidAPI-Host", "api-nba-v1.p.rapidapi.com" },
                },
            };

            using(var response = _HttpClient.SendAsync(request))
            {
                return await response.Result.Content.ReadAsStringAsync();
            }
        }
    }
}