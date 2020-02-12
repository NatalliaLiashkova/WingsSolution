using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsAPI.Model;
using WingsAPI.Models;
using WingsOnServices.Interfaces;

namespace WingsAPI.Controllers
{
    [ApiController]
    public class PassengersController : ControllerBase
    {
        #region Properties
        private readonly IPassengerService passengersService;
        private readonly IPersonService personService;
        private readonly IMapper mapper;
        #endregion

        #region Ctors
        public PassengersController(IPassengerService service, IPersonService personService, IMapper mapper)
        {
            this.passengersService = service;
            this.personService = personService;
            this.mapper = mapper;
        }
        #endregion  

        #region HttpGet methods 
        [HttpGet]
        [Route("api/passengers")]
        public ActionResult<IEnumerable<PersonModel>> GetPassengers(string gender = "")
        {
            if(gender == null)
            {
                throw new ArgumentException();
            }

            var response = mapper.Map<IList<PersonModel>>(passengersService.GetPassengersByGender(gender));
            return Ok(response);
        }
        #endregion

        #region HttpPatch methods
        [HttpPatch]
        [Route("api/passengers/update/{id}")]
        public async Task<IActionResult> UpdatePassenger(int id, [FromBody] AddressModel address)
        {
            if (String.IsNullOrWhiteSpace(address.Address))
            {
                throw new ArgumentException();
            }

            var personToBeUpdated = personService.GetPersonById(id);

            if (personToBeUpdated == null)
            {
                return NotFound();
            }
            var savePerson = new PersonModel() { Address = address.Address };
            await passengersService.UpdatePerson(personToBeUpdated, address.Address);
            
            return NoContent();
        }
        #endregion
    }
}
