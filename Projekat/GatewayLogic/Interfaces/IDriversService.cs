using DataLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GatewayLogic.Interfaces
{
    public interface IDriversService
    {
        public Task<GatewayResponse<List<DriverApiReadDTO>, List<DriverReadDTO>>> GetDrivers(string search);

        public Task<GatewayResponse<DriverApiReadDTO, DriverReadDTO>> GetDriverByCode(string code);

        public Task<DriverWriteDTO> AddDriver(DriverWriteDTO driver);

        public Task<bool> DeleteDriver(int driverId);
    }
}
