using System.Threading.Tasks;

namespace GatewayLogic.Interfaces
{
    public interface ICircuitService
    {
        public Task<string> GetCircuits();
    }
}