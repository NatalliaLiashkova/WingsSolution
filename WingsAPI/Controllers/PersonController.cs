using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsAPI.Model;
using WingsOnServices.Interfaces;

namespace WingsAPI.Controllers
{
    [ApiController]
    public class PersonController : ControllerBase
    {
        #region Properties
        private readonly IPersonService personService;
        private readonly IMapper mapper;
        #endregion

        #region Properties
        public PersonController(IPersonService service, IMapper mapper)
        {
            this.personService = service;
            this.mapper = mapper;
        }
        #endregion

        [HttpGet]
        [Route("api/person/{personId}")]
        public IActionResult GetPersonById(int personId)
        {
            if (personId < 0)
            {
                throw new ArgumentException();
            }

            var response = mapper.Map<PersonModel>(personService.GetPersonById(personId));

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}