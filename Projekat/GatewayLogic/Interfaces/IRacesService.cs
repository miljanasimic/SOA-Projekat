using DataLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GatewayLogic.Interfaces
{
    public interface IRacesService
    {
        public Task<BaseResponse<GatewayResponse<List<RaceApiReadDTO>, List<RaceReadDTO>>>> GetRacesFromSeason(int season);

        public Task<BaseResponse<GatewayResponse<RaceApiReadDTO, RaceReadDTO>>> GetRaceById(int id);

        public Task<BaseResponse<RaceWriteDTO>> AddRace(RaceWriteDTO race);

        public Task<BaseResponse<string>> DeleteRace(int id);

        public Task<BaseResponse<RaceReadDTO>> EditRace(int id, RaceEditDTO newRaceData);
    }
}
