using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/trips/{tripName}/Stops")]
    public class StopsController: Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger<StopsController> _logger;
        private IWordlRepository _repository;

        public StopsController(IWordlRepository repository, ILogger<StopsController> logger, GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetUserTripByName(tripName, User.Identity.Name);

                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s=> s.Order).ToList()));
            }
            catch (Exception ex)
            { 
                _logger.LogError($"Could not get the Stops {ex}");
            }

            return BadRequest("Failed to get the Stops.");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                //Check if the validation that we required in the model is fulfilled
                if(ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);
                    //Look up the Geocodes from service
                    
                    var result = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!result.Sucess)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;
                   
                        //save the Stops to the _repository ONLY
                        _repository.AddStop(tripName, newStop, User.Identity.Name);
                        //save in the database and return the saved data filtered by the ViewModel
                        if (await _repository.SaveChangesAsync())
                        {

                            return Created($"/api/trips/{tripName}/stops/{newStop.Name}",
                                Mapper.Map<StopViewModel>(newStop));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to save new Stop: {0}", ex);
               
            }
            return BadRequest("Failed to save new Stop");
        }

           
    }
}
