using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.DTO;
using WingsOnServices.Interfaces;

namespace WingsOnServices
{
    public class FlightService : IFlightService
    {
        #region Properties
        private readonly IRepository<Booking> bookingRepository;
        private readonly IRepository<Flight> flightRepository;
        private readonly IMapper mapper;
        #endregion

        #region Ctors
        public FlightService(IRepository<Booking> bookingRepository, IRepository<Flight> flightRepository, IMapper mapper)
        {
            this.bookingRepository = bookingRepository;
            this.flightRepository = flightRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Public methods
        public IEnumerable<PersonDTO> GetPassengersByFlight(string flightNumber)
        {
            try
            {
                var flight = flightRepository.GetAll().Single(fl => fl.Number == flightNumber);

                if (flight == null)
                {
                    throw new ArgumentException();
                }

                var bookings = bookingRepository.GetAll().Where(b => b.Flight.Id == flight.Id);
                var passengers = bookings != null ? bookings.SelectMany(b => b.Passengers) : new List<Person>();

                return mapper.Map<List<PersonDTO>>(passengers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
