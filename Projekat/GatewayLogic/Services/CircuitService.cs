using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DataLayer.DTOs;
using DataLayer.Models;
using GatewayLogic.Interfaces;
using Newtonsoft.Json;

namespace GatewayLogic.Services
{
    public class CircuitService : ICircuitService
    {
        private HttpClient _HttpClient;

        public CircuitService(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public async Task<GatewayResponse<List<CircuitsApiReadDTO>, List<CircuitsReadDTO>>> GetCircuits()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_HttpClient.BaseAddress.ToString() + "/circuits"),
                Headers =
                {
                    { "X-RapidAPI-Key", "cf0b762acbmsh4bdc9e4604c5ab5p1dc264jsne63230b32cc6" },
                    { "X-RapidAPI-Host", "api-formula-1.p.rapidapi.com" },
                }
            };
            using (var response = await _HttpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseModels = JsonConvert.DeserializeObject<ApiResponse<CircuitModel[]>>(responseContent);

                var apiDtos = new List<CircuitsApiReadDTO>();

                foreach(var responseModel in responseModels.Response)
                {
                    apiDtos.Add(responseModel.ConvertToReadDTO());
                }

                return new GatewayResponse<List<CircuitsApiReadDTO>, List<CircuitsReadDTO>>
                {
                    ApiReponse = apiDtos
                };
            }
        }
    }
}