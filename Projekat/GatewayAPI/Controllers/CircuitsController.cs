using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GatewayLogic.Interfaces;

namespace GatewayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CircuitsController : ControllerBase
    {
        private readonly ICircuitService _circuitService;

        public CircuitsController(ICircuitService circuitService)
        {
            _circuitService = circuitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCircuits()
        {
            var result = await _circuitService.GetCircuits();
            return Ok(result);
        }
    }
}
