using DataLayer.DTOs;
using System.Threading.Tasks;

namespace GatewayLogic.Interfaces
{
    public interface ILapService
    {
        public Task<BaseResponse<bool>> AddLap(LapWriteDTO lap);
    }
}
