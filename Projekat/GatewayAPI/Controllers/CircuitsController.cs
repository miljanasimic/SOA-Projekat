using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GatewayLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCircuits()
        {
            try
            {
                var result = await _circuitService.GetCircuits();

                if (result == null)
                    return StatusCode(500, "An error occured.");

                if (result.ErrorMessage != null)
                    return NotFound(result.ErrorMessage);

                return Ok(result.ResponseContent);
            }
            catch(Exception e)
            {
                return StatusCode(500, "An error occured.");
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCircuitById([FromRoute] int id)
        {
            try
            {
                var result = await _circuitService.GetCircuitById(id);

                if (result == null)
                    return NotFound("An error occured.");

                if (result.ErrorMessage != null)
                    return NotFound(result.ErrorMessage);

                return Ok(result.ResponseContent);
            }
            catch(Exception e)
            {
                return StatusCode(500, "An error occured.");
            }
        }
    }
}
