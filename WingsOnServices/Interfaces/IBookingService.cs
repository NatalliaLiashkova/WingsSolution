using System.Threading.Tasks;
using WingsOn.DTO;

namespace WingsOnServices.Interfaces
{
    public interface IBookingService
    {
        Task CreateBooking(string flight, PersonDTO person);
    }
}
