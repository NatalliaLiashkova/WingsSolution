using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WingsAPI.Services;
using WingsOn.Domain;

namespace WingsAPI.Controllers
{
    [ApiController]
    public class PassengersController : ControllerBase
    {
        #region Properties
        private readonly IPassengersService passengersService;
        #endregion

        #region Ctors
        public PassengersController(IPassengersService service)
        {
            this.passengersService = service;
        }
        #endregion  

        #region HttpGet methods 
        [HttpGet]
        [Route("api/passengers/{personId}")]
        public IActionResult GetPersonById(int personId)
        {
            if (personId < 0)
            {
                throw new Exception("Please enter valid value for id.");
            }

            var response = passengersService.GetPersonById(personId);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);     
        }

        [HttpGet]
        [Route("api/passengers")]
        public IActionResult GetPassengers(string gender = "")
        {
            gender = gender ?? "";
            var response = passengersService.GetPassengersByGender(gender);
            return Ok(response);
        }

        [HttpGet]
        [Route("api/passengers/getPassengersByFlight/{flight}")]
        public IActionResult GetPassengersByFlight(string flight)
        {
            if (String.IsNullOrEmpty(flight))
            {
                return NotFound();
            }
            
            var response = passengersService.GetPassengersByFlight(flight);
            return Ok(response);
        }
        #endregion

        #region HttpPut methods
        [HttpPut("{id}")]
        [Route("api/persons/update/{id}")]
        public async Task<ActionResult<Person>> UpdatePerson(int id, [FromBody] Person savePerson)
        {
            var personToBeUpdated = passengersService.GetPersonById(id);

            if (personToBeUpdated == null)
            {
                return NotFound();
            }

            var updatedPerson = await passengersService.UpdatePerson(personToBeUpdated, savePerson);
            return Ok();
        }
        #endregion
    }
}
