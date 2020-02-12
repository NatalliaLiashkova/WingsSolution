using System.Collections.Generic;
using WingsOn.DTO;

namespace WingsOnServices.Interfaces
{
    public interface IFlightService
    {
        IEnumerable<PersonDTO> GetPassengersByFlight(string flightNumber);
    }
}
