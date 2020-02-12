using WingsOn.DTO;

namespace WingsOnServices.Interfaces
{
    public interface IPersonService
    {
        PersonDTO GetPersonById(int id);
    }
}
