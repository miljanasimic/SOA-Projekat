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
            try
            {
                var apiDtos = new List<CircuitsApiReadDTO>();
                using (var response = await Common.SendApiRequest(_HttpClientFactory, "/circuits"))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
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

                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var serviceDtos = new List<CircuitsReadDTO>();
                using (var response = await httpClient.GetAsync("/circuits"))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
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
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<GatewayResponse<CircuitsApiReadDTO, CircuitsReadDTO>> GetCircuitById(int id)
        {
            try
            {
                CircuitsApiReadDTO circuitApiDto = null;
                using (var response = await Common.SendApiRequest(_HttpClientFactory, "/circuits?id=" + id))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseModel = JsonConvert.DeserializeObject<ApiResponse<List<CircuitModel>>>(responseContent);
                        if(responseModel.Response.Count > 0)
                            circuitApiDto = responseModel.Response[0].ConvertToReadDTO();
                    }
                }

                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");

                CircuitsReadDTO circuitServiceDTO = null;
                using (var response = await httpClient.GetAsync("/circuits/" + id))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        circuitServiceDTO = JsonConvert.DeserializeObject<CircuitsReadDTO>(responseContent);
                    }
                }

                if (circuitApiDto == null && circuitServiceDTO == null)
                    return null;

                return new GatewayResponse<CircuitsApiReadDTO, CircuitsReadDTO>
                {
                    ApiReponse = circuitApiDto,
                    ServiceResponse = circuitServiceDTO
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<object> AddCircuit(CircuitWriteDTO circuitDto)
        {
            return null;
            //try
            //{

            //}
            //catch(Exception e)
            //{

            //}
        }

        #endregion
    }
}