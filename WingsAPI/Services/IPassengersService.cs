using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WingsOn.Domain;

namespace WingsAPI.Services
{
    public interface IPassengersService
    {
        Person GetPersonById(int id);
        IEnumerable<Person> GetAllPassengers();
        IEnumerable<Person> GetPassengersByGender(string gender);
        IEnumerable<Person> GetPassengersByFlight(string flight);
        Task<Person> UpdatePerson(Person personToBeUpdated, Person person);
    }
}
