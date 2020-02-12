using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.DTO;
using WingsOnServices.Interfaces;

namespace WingsOnServices
{
    public class PassengersService : IPassengerService
    {
        #region Properties
        private readonly IRepository<Person> personRepository;
        private readonly IRepository<Booking> bookingRepository;
        private readonly IMapper mapper;
        #endregion

        #region Ctor
        public PassengersService(IRepository<Person> repository, IRepository<Booking> bookingRepository, IMapper mapper)
        {
            this.personRepository = repository;
            this.bookingRepository = bookingRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Public methods
        public IEnumerable<PersonDTO> GetAllPassengers()
        {
            try
            {
                return mapper.Map<List<PersonDTO>>(bookingRepository.GetAll().SelectMany(b => b.Passengers));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PersonDTO> GetPassengersByGender(string gender)
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

        public async Task UpdatePerson(PersonDTO personToBeUpdated, string address)
        {
            try
            {

                personToBeUpdated.Address = address;

                await Task.Run(() => personRepository.Save(mapper.Map<Person>(personToBeUpdated)));
                UpdatePassenger(personToBeUpdated.Id, address);
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
