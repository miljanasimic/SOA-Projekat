using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GatewayLogic.Interfaces;
using Microsoft.AspNetCore.Http;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCircuits()
        {
            var result = await _circuitService.GetCircuits();

            if (result == null)
                return NotFound("Doslo je do greske.");

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCircuitById([FromRoute] int id)
        {
            var result = await _circuitService.GetCircuitById(id);

            if (result == null)
                return NotFound("Doslo je do greske.");

            return Ok(result);
        }
    }
}
