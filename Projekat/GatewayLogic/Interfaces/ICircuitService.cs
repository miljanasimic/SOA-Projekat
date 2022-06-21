using DataLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GatewayLogic.Interfaces
{
    public interface ICircuitService
    {
        public Task<BaseResponse<GatewayResponse<List<CircuitsApiReadDTO>, List<CircuitsReadDTO>>>> GetCircuits();

        public Task<BaseResponse<GatewayResponse<CircuitsApiReadDTO, CircuitsReadDTO>>> GetCircuitById(int id);
    }
}