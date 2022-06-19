using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly IHttpClientFactory _HttpClientFactory;

        public CircuitService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }

        #region Public Methods

        public async Task<GatewayResponse<List<CircuitsApiReadDTO>, List<CircuitsReadDTO>>> GetCircuits()
        {
            var httpClient = _HttpClientFactory.CreateClient("ApiHttpClient");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(httpClient.BaseAddress.ToString() + "/circuits"),
                Headers =
                {
                    { "X-RapidAPI-Key", "cf0b762acbmsh4bdc9e4604c5ab5p1dc264jsne63230b32cc6" },
                    { "X-RapidAPI-Host", "api-formula-1.p.rapidapi.com" },
                }
            };

            var apiDtos = new List<CircuitsApiReadDTO>();
            using (var response = await httpClient.SendAsync(request))
            {
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseModels = JsonConvert.DeserializeObject<ApiResponse<List<CircuitModel>>>(responseContent);

                    foreach (var responseModel in responseModels.Response)
                    {
                        apiDtos.Add(responseModel.ConvertToReadDTO());
                    }
                }
                else
                {
                    apiDtos = null;
                }
            }

            httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
            var serviceDtos = new List<CircuitsReadDTO>();
            using (var response = await httpClient.GetAsync("/circuits"))
            {
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    serviceDtos = JsonConvert.DeserializeObject<List<CircuitsReadDTO>>(responseContent);
                }
                else
                {
                    serviceDtos = null;
                }
            }

            if (apiDtos == null && serviceDtos == null)
                return null;

            return new GatewayResponse<List<CircuitsApiReadDTO>, List<CircuitsReadDTO>>
            {
                ApiReponse = apiDtos,
                ServiceResponse = serviceDtos
            };
        }

        //public async Task<GatewayResponse<CircuitsApiReadDTO, CircuitsReadDTO>> GetCircuitById(int id)
        //{
            
        //}
    }

    #endregion
}