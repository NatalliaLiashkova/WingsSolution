using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.DTO;
using WingsOnServices.Interfaces;

namespace WingsOnServices
{
    public class BookingService : IBookingService
    {
        #region Properties
        private readonly IRepository<Person> personRepository;
        private readonly IRepository<Booking> bookingRepository;
        private readonly IRepository<Flight> flightRepository;
        private readonly IMapper mapper;
        #endregion

        #region Ctors
        public BookingService(IRepository<Person> repository, IRepository<Booking> bookingRepository, IRepository<Flight> flightRepository, IMapper mapper)
        {
            this.personRepository = repository;
            this.bookingRepository = bookingRepository;
            this.flightRepository = flightRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Public methods
        public async Task CreateBooking(string flight, PersonDTO person)
        {
            CreateNewPerson(person);
            var booking = new Booking()
            {
                Id = bookingRepository.GetAll().Select(p => p.Id).Max() + 1,
                Flight = flightRepository.GetAll().Where(fl => fl.Number == flight).FirstOrDefault(),
                DateBooking = DateTime.Now,
                Customer = personRepository.GetAll().Where(p => p.Id == person.Id).FirstOrDefault(),
                Passengers = new[]
                    {
                        mapper.Map<Person>(person)
                    }
            };
            await Task.Run(() => bookingRepository.Save(booking));
        }
        #endregion

        #region Private Methods
        private void CreateNewPerson(PersonDTO person)
        {
            person.Id = personRepository.GetAll().Select(p => p.Id).Max() + 1;
            personRepository.Save(mapper.Map<Person>(person));
        }
        #endregion
    }
}
