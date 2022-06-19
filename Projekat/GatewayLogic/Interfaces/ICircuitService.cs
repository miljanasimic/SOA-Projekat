using DataLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GatewayLogic.Interfaces
{
    public interface ICircuitService
    {
        public Task<GatewayResponse<List<CircuitsApiReadDTO>, List<CircuitsReadDTO>>> GetCircuits();
    }
}