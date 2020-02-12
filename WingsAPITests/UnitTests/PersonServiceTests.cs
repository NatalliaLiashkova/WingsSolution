using AutoMapper;
using Moq;
using System;
using System.Linq;
using WingsAPI.Profiles;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOnServices;
using WingsOnServices.Interfaces;
using Xunit;

namespace WingsAPITests.UnitTests
{
    public class PersonServiceTests
    {
        private readonly DataSource dataSource;

        public PersonServiceTests()
        {
            dataSource = new DataSource();
        }       

        [Theory]
        [InlineData(91)]
        public void GetPersonByIdTest(int id)
        {
            //Arrange
            var mockPersonRepository = new Mock<IRepository<Person>>();

            mockPersonRepository.Setup(repo => repo.Get(id))
                    .Returns(dataSource.Persons.Where(p => p.Id == id).FirstOrDefault());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();

            IPersonService personService = new PersonService(mockPersonRepository.Object, mapper);

            // Act
            var response = personService.GetPersonById(id);
            
            // Assert
            Assert.Equal("Kendall Velazquez", response.Name);
        }
    }
}
