using DataLayer.DTOs;
using GatewayLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchDrivers([FromQuery] string name)
        {
            try
            {
                if (name == null || name.Length < 3)
                {
                    return BadRequest("Search string needs to have atleast 3 letters.");
                }

                var result = await _DriversService.GetDrivers(name);

                if (result == null)
                    return StatusCode(500, "An error has occured.");

                if (result.ErrorMessage != null)
                    return NotFound(result.ErrorMessage);

                return Ok(result.ResponseContent);
            }
            catch(Exception e)
            {
                return StatusCode(500, "An error has occured.");
            }
        }

        [HttpGet]
        [Route("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDriverByCode([FromRoute] string code)
        {
            try
            {
                var result = await _DriversService.GetDriverByCode(code);

                if (result == null)
                    return StatusCode(500, "An error has occured.");

                if (result.ErrorMessage != null)
                    return NotFound(result.ErrorMessage);

                return Ok(result.ResponseContent);
            }
            catch(Exception e)
            {
                return StatusCode(500, "An error has occured.");
            }  
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDriver([FromBody] DriverWriteDTO driver)
        {
            try
            {
                var result = await _DriversService.AddDriver(driver);

                if (result == null)
                    return StatusCode(500, "Driver was not added. An error occured.");

                if (result.ErrorMessage != null)
                    return StatusCode(400, result.ErrorMessage);

                return CreatedAtAction(nameof(GetDriverByCode), new { code = driver.Code }, result.ResponseContent);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Driver was not added. An error occured.");
            }
            
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDriver([FromRoute] int id)
        {
            try
            {
                var result = await _DriversService.DeleteDriver(id);

                if (result == null)
                    return StatusCode(500, "An error occured.");

                if (!result.IsSuccess)
                    return NotFound(result.ErrorMessage);

                return StatusCode(202, result.ResponseContent);
            }
            catch(Exception e)
            {
                return StatusCode(500, "An error occured.");
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditDriver([FromRoute] int id, [FromBody] DriverEditDTO newDriverData)
        {
            try
            {
                var result = await _DriversService.EditDriver(id, newDriverData);

                if (result == null)
                    return StatusCode(500, "An error occured.");

                if (result.ErrorMessage != null)
                    return NotFound(result.ErrorMessage);

                return Accepted(result.ResponseContent);
            }
            catch(Exception e)
            {
                return StatusCode(500, "An error occured.");
            }
        }
    }
}
