using System.Collections.Generic;
using System.Threading.Tasks;
using WingsOn.Domain;
using WingsOn.DTO;

namespace WingsOnServices.Interfaces
{
    public interface IPassengerService
    {
        IEnumerable<PersonDTO> GetAllPassengers();
        IEnumerable<PersonDTO> GetPassengersByGender(string gender);
        Task UpdatePerson(PersonDTO personToBeUpdated, string address);
    }
}
