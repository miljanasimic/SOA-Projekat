using DataLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GatewayLogic.Interfaces
{
    public interface IDriversService
    {
        public Task<BaseResponse<GatewayResponse<List<DriverApiReadDTO>, List<DriverReadDTO>>>> GetDrivers(string search);

        public Task<BaseResponse<GatewayResponse<DriverApiReadDTO, DriverReadDTO>>> GetDriverByCode(string code);

        public Task<BaseResponse<DriverWriteDTO>> AddDriver(DriverWriteDTO driver);

        public Task<BaseResponse<string>> DeleteDriver(int driverId);

        public Task<BaseResponse<DriverReadDTO>> EditDriver(int driverId, DriverEditDTO newDriverData);
    }
}
