using DataLayer.DTOs;
using DataLayer.Models;
using GatewayLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GatewayLogic.Services
{
    public class DriversService : IDriversService
    {
        private readonly IHttpClientFactory _HttpClientFactory;

        public DriversService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }

        public async Task<GatewayResponse<List<DriverApiReadDTO>, List<DriverReadDTO>>> GetDrivers(string search)
        {
            try
            {
                var apiDtos = new List<DriverApiReadDTO>();
                using (var response = await Common.SendApiRequest(_HttpClientFactory, "/drivers?search=" + search)) 
                {
                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseModels = JsonConvert.DeserializeObject<ApiResponse<List<DriverModel>>>(responseContent);

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

                var httpClient  = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var serviceDtos = new List<DriverReadDTO>();
                using (var response = await httpClient.GetAsync("/drivers?search=" + search))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        serviceDtos = JsonConvert.DeserializeObject<List<DriverReadDTO>>(responseContent);
                    }
                    else
                    {
                        serviceDtos = null;
                    }
                }

                if (apiDtos == null && serviceDtos == null)
                    return null;

                return new GatewayResponse<List<DriverApiReadDTO>, List<DriverReadDTO>>
                {
                    ApiReponse = apiDtos,
                    ServiceResponse = serviceDtos
                };
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<GatewayResponse<DriverApiReadDTO, DriverReadDTO>> GetDriverByCode(string code)
        {
            try
            {
                DriverApiReadDTO driverApiDto = null;
                using (var response = await Common.SendApiRequest(_HttpClientFactory, "/drivers?search=" + code))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseModel = JsonConvert.DeserializeObject<ApiResponse<List<DriverModel>>>(responseContent);
                        if (responseModel.Response.Count > 0)
                            driverApiDto = responseModel.Response[0].ConvertToReadDTO();
                    }
                }

                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");

                DriverReadDTO serviceDriverDTO = null;
                using(var response = await httpClient.GetAsync("/drivers/" + code))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        serviceDriverDTO = JsonConvert.DeserializeObject<DriverReadDTO>(responseContent);
                    }
                }

                if (driverApiDto == null && serviceDriverDTO == null)
                    return null;

                return new GatewayResponse<DriverApiReadDTO, DriverReadDTO>
                {
                    ApiReponse = driverApiDto,
                    ServiceResponse = serviceDriverDTO
                };
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<DriverWriteDTO> AddDriver(DriverWriteDTO driver)
        {
            try
            {
                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var content = JsonContent.Create<DriverWriteDTO>(driver);

                using(var response = await httpClient.PostAsync("/drivers", content))
                {
                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        return driver;
                    }

                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<bool> DeleteDriver(int driverId)
        {
            try
            {
                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");

                using(var response = await httpClient.DeleteAsync("/drivers/" + driverId))
                {
                    return response.IsSuccessStatusCode;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
