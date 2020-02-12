using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsAPI.Model;
using WingsOnServices.Interfaces;

namespace WingsAPI.Controllers
{
    [ApiController]
    public class FlightController : ControllerBase
    {
        #region Properties
        private readonly IFlightService passengersService;
        private readonly IMapper mapper;
        #endregion

        #region Ctors
        public FlightController(IFlightService service, IMapper mapper)
        {
            this.passengersService = service;
            this.mapper = mapper;
        }
        #endregion

        [HttpGet]
        [Route("api/flight/getPassengers/{flightNumber}")]
        public ActionResult<IEnumerable<PersonModel>> GetPassengersByFlight(string flightNumber)
        {
            if (String.IsNullOrEmpty(flightNumber))
            {
                throw new ArgumentException();
            }

            var response = mapper.Map<List<PersonModel>>(passengersService.GetPassengersByFlight(flightNumber));
            return Ok(response);
        }
    }
}