using DataLayer.DTOs;
using GatewayLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GatewayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly IDriversService _DriversService;

        public DriversController(IDriversService driversService)
        {
            _DriversService = driversService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchDrivers([FromQuery] string name)
        {
            if (name.Length < 3)
            {
                return BadRequest("String za pretragu mora da sadrzi bar 3 slova.");
            }

            var result = await _DriversService.GetDrivers(name);

            if (result == null)
                return NotFound("Doslo je do greske.");

            return Ok(result);
        }

        [HttpGet]
        [Route("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDriverByCode([FromRoute] string code)
        {
            var result = await _DriversService.GetDriverByCode(code);

            if (result == null)
                return NotFound("Doslo je do greske.");

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDriver([FromBody] DriverWriteDTO driver)
        {
            var result = await _DriversService.AddDriver(driver);

            if (result == null)
                return BadRequest("Doslo je do greske.");

            return CreatedAtAction(nameof(GetDriverByCode), new { code = driver.Code }, driver);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDriver([FromRoute] int id)
        {
            var result = await _DriversService.DeleteDriver(id);

            if (!result)
                return BadRequest("Doslo je do greske.");

            return StatusCode(204);
        }
    }
}
