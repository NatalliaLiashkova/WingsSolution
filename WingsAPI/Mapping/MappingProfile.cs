using AutoMapper;
using WingsAPI.Model;
using WingsOn.Domain;
using WingsOn.DTO;

namespace WingsAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonModel>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}
