using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsAPI.Services
{
    public class PassengersService : IPassengersService
    {
        #region Properties
        private readonly PersonRepository personRepository;
        private readonly BookingRepository bookingRepository;
        private readonly FlightRepository flightRepository;
        #endregion

        #region Ctor
        public PassengersService(PersonRepository repository, BookingRepository bookingRepository, FlightRepository flightRepository)
        {
            this.personRepository = repository;
            this.bookingRepository = bookingRepository;
            this.flightRepository = flightRepository;
        }
        #endregion

        #region Public methods
        public Person GetPersonById(int id)
        {
            try
            {
                return personRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Person> GetAllPassengers()
        {
            try
            {
                return bookingRepository.GetAll().SelectMany(b => b.Passengers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public Flight GetFlightById(int id)
        {
            try
            {
                return flightRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Person> GetPassengersByGender(string gender)
        {
            try
            {
                switch (gender.ToUpper())
                {
                    case "MALE":
                        return GetAllPassengers().Where(p => (int)p.Gender == (int)GenderType.Male);
                    case "FEMALE":
                        return GetAllPassengers().Where(p => (int)p.Gender == (int)GenderType.Female);
                    default:
                        return GetAllPassengers();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Person> GetPassengersByFlight(string flight)
        {
            try
            {
                var bookings = bookingRepository.GetAll().Where(b => b.Flight.Number == flight);
                var passengers = bookings.SelectMany(b => b.Passengers);

                return passengers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Person> UpdatePerson(Person personToBeUpdated, Person person)
        {
            try
            {
                personToBeUpdated.Address = person.Address;

                await Task.Run(() => personRepository.Save(personToBeUpdated));
                UpdatePassenger(personToBeUpdated.Id, person.Address);

                return personRepository.Get(personToBeUpdated.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Private methods
        private void UpdatePassenger(int id, string adress)
        {
            try
            {
                var bookings = bookingRepository.GetAll().Where(b => b.Passengers.Where(p => p.Id == id).Any()).ToList();
                foreach (var booking in bookings)
                {
                    foreach (var passenger in booking.Passengers)
                    {
                        if (passenger.Id == id)
                        {
                            passenger.Address = adress;
                            bookingRepository.Save(booking);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
