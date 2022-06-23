using DataLayer.DTOs;
using GatewayLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly IRacesService _RacesService;

        public RacesController(IRacesService racesService)
        {
            _RacesService = racesService;
        }

        [HttpGet]
        [Route("/season/{season}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRacesFromSeason([FromRoute] int season)
        {
            try
            {
                var result = await _RacesService.GetRacesFromSeason(season);
                
                if(result == null)
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
        public async Task<IActionResult> GetRaceById([FromRoute] int id)
        {
            try
            {
                var result = await _RacesService.GetRaceById(id);

                if (result == null)
                    return StatusCode(500, "An error occured.");

                if (result.ErrorMessage != null)
                    return NotFound(result.ErrorMessage);

                return Ok(result.ResponseContent);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occured.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRace([FromBody] RaceWriteDTO race)
        {
            try
            {
                var result = await _RacesService.AddRace(race);

                if (result == null)
                    return StatusCode(500, "Race was not added. An error occured.");

                if (result.ErrorMessage != null)
                    return StatusCode(400, result.ErrorMessage);

                return CreatedAtAction(nameof(GetRaceById), new { id = race.RaceId }, result.ResponseContent);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Race was not added. An error occured.");
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
                var result = await _RacesService.DeleteRace(id);

                if (result == null)
                    return StatusCode(500, "An error occured.");

                if (!result.IsSuccess)
                    return NotFound(result.ErrorMessage);

                return StatusCode(202, result.ResponseContent);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occured.");
            }
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditRace([FromRoute] int id, [FromBody] RaceEditDTO newRaceData)
        {
            try
            {
                var result = await _RacesService.EditRace(id, newRaceData);

                if (result == null)
                    return StatusCode(500, "An error occured.");

                if (result.ErrorMessage != null)
                    return NotFound(result.ErrorMessage);

                return Accepted(result.ResponseContent);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occured.");
            }
        }
    }
}
