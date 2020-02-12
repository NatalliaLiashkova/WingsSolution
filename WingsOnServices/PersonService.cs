using AutoMapper;
using System;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.DTO;
using WingsOnServices.Interfaces;

namespace WingsOnServices
{
    public class PersonService : IPersonService
    {
        #region Properties
        private readonly IRepository<Person> personRepository;
        private readonly IMapper mapper;
        #endregion

        #region Ctors
        public PersonService(IRepository<Person> repository, IMapper mapper)
        {
            this.personRepository = repository;
            this.mapper = mapper;
        }
        #endregion

        #region Public methods
        public PersonDTO GetPersonById(int id)
        {
            try
            {
                return mapper.Map<PersonDTO>(personRepository.Get(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
