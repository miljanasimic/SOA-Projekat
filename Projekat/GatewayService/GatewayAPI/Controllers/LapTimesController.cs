using DataLayer.DTOs;
using GatewayLogic;
using GatewayLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GatewayAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LapTimesController : ControllerBase
    {
        private readonly ILapService _LapService;

        public LapTimesController(ILapService lapService)
        {
            _LapService = lapService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddLapTime([FromBody] LapWriteDTO lapDto)
        {
            try
            {
                var result = await _LapService.AddLap(lapDto);

                if (result == null)
                    return StatusCode(500, "Lap time was not added. An error occured.");

                if (result.ErrorMessage != null)
                    return StatusCode(400, result.ErrorMessage);

                //publish to topic
                await MqttHelper.PublishToTopic("mqtt", 1883, "lap-data", JsonConvert.SerializeObject(lapDto));

                return StatusCode(201);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Lap time was not added. An error occured.");
            }
        }
    }
}
