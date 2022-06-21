using DataLayer.DTOs;
using DataLayer.Models;
using GatewayLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GatewayLogic.Services
{
    public class RacesService : IRacesService
    {
        private readonly IHttpClientFactory _HttpClientFactory;

        public RacesService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }

        public async Task<BaseResponse<GatewayResponse<List<RaceApiReadDTO>, List<RaceReadDTO>>>> GetRacesFromSeason(int season)
        {
            try
            {
                var apiDtos = new List<RaceApiReadDTO>();

                using(var response = await Common.SendApiRequest(_HttpClientFactory, "/races?type=race&season=" + season))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseModels = JsonConvert.DeserializeObject<ApiResponse<List<RaceModel>>>(responseContent);

                        foreach(var model in responseModels.Response)
                        {
                            apiDtos.Add(model.ConvertToReadDTO());
                        }
                    }
                    else
                    {
                        apiDtos = null;
                    }
                }

                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var serviceDtos = new List<RaceReadDTO>();
                string errorMessage = null;
                using (var response = await httpClient.GetAsync("/races/season/" + season))
                {
                    var responseContent = await response.Content?.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        serviceDtos = JsonConvert.DeserializeObject<List<RaceReadDTO>>(responseContent);
                    }
                    else
                    {
                        errorMessage = responseContent;
                        serviceDtos = null;
                    }
                }

                if (apiDtos == null && serviceDtos == null)
                    return new BaseResponse<GatewayResponse<List<RaceApiReadDTO>, List<RaceReadDTO>>>
                    {
                        ErrorMessage = errorMessage,
                        IsSuccess = false
                    };

                return new BaseResponse<GatewayResponse<List<RaceApiReadDTO>, List<RaceReadDTO>>>
                {
                    ResponseContent = new GatewayResponse<List<RaceApiReadDTO>, List<RaceReadDTO>>
                    {
                        ApiReponse = apiDtos,
                        ServiceResponse = serviceDtos
                    },
                    IsSuccess = true
                };
            }
            catch(Exception e)
            {
                return new BaseResponse<GatewayResponse<List<RaceApiReadDTO>, List<RaceReadDTO>>>
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<BaseResponse<GatewayResponse<RaceApiReadDTO, RaceReadDTO>>> GetRaceById(int id)
        {
            try
            {
                RaceApiReadDTO raceApiDto = null;
                using (var response = await Common.SendApiRequest(_HttpClientFactory, "/races?id=" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseModel = JsonConvert.DeserializeObject<ApiResponse<List<RaceModel>>>(responseContent);
                        if (responseModel.Response.Count > 0)
                            raceApiDto = responseModel.Response[0].ConvertToReadDTO();
                    }
                }

                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
                RaceReadDTO raceServiceDTO = null;
                string errorMessage = null;
                using(var response = await httpClient.GetAsync("/races/" + id))
                {
                    var responseContent = await response.Content?.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        raceServiceDTO = JsonConvert.DeserializeObject<RaceReadDTO>(responseContent);
                    }
                    else
                    {
                        errorMessage = responseContent;
                    }
                }

                if (raceApiDto == null && raceServiceDTO == null)
                    return new BaseResponse<GatewayResponse<RaceApiReadDTO, RaceReadDTO>>
                    {
                        ErrorMessage = errorMessage,
                        IsSuccess = false
                    };

                return new BaseResponse<GatewayResponse<RaceApiReadDTO, RaceReadDTO>>
                {
                    ResponseContent = new GatewayResponse<RaceApiReadDTO, RaceReadDTO>
                    {
                        ApiReponse = raceApiDto,
                        ServiceResponse = raceServiceDTO
                    },
                    IsSuccess = true
                };
            }
            catch(Exception e)
            {
                return new BaseResponse<GatewayResponse<RaceApiReadDTO, RaceReadDTO>>
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<BaseResponse<RaceWriteDTO>> AddRace(RaceWriteDTO race)
        {
            try
            {
                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var content = JsonContent.Create<RaceWriteDTO>(race);

                using (var response = await httpClient.PostAsync("/races", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return new BaseResponse<RaceWriteDTO>
                        {
                            ResponseContent = race,
                            IsSuccess = true
                        };
                    }

                    return new BaseResponse<RaceWriteDTO>
                    {
                        ErrorMessage = await response.Content?.ReadAsStringAsync(),
                        IsSuccess = false
                    };
                }
            }
            catch(Exception e)
            {
                return new BaseResponse<RaceWriteDTO>
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<BaseResponse<string>> DeleteRace(int id)
        {
            try
            {
                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");

                using (var response = await httpClient.DeleteAsync("/races/" + id))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return new BaseResponse<string>
                        {
                            ErrorMessage = await response.Content?.ReadAsStringAsync(),
                            IsSuccess = false
                        };
                    }

                    return new BaseResponse<string>
                    {
                        IsSuccess = true,
                        ResponseContent = await response.Content?.ReadAsStringAsync()
                    };

                }
            }
            catch (Exception e)
            {
                return new BaseResponse<string>
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<BaseResponse<RaceReadDTO>> EditRace(int id, RaceEditDTO newRaceData)
        {
            try
            {
                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var content = JsonContent.Create<RaceEditDTO>(newRaceData);

                using (var response = await httpClient.PatchAsync("/races/" + id, content))
                {
                    var responseContent = await response.Content?.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return new BaseResponse<RaceReadDTO>
                        {
                            ResponseContent = JsonConvert.DeserializeObject<RaceReadDTO>(responseContent),
                            IsSuccess = true
                        };
                    }

                    return new BaseResponse<RaceReadDTO>
                    {
                        ErrorMessage = responseContent,
                        IsSuccess = false
                    };
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<RaceReadDTO>
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
            }
        }
    }
}
