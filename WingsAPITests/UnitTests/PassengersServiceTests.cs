using AutoMapper;
using Moq;
using WingsAPI.Profiles;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.DTO;
using WingsOnServices;
using WingsOnServices.Interfaces;
using Xunit;

namespace WingsAPITests.UnitTests
{
    public class PassengersServiceTests
    {
        private readonly DataSource dataSource;

        public PassengersServiceTests()
        {
            dataSource = new DataSource();
        }

        [Theory]
        [InlineData("male", GenderTypeDTO.Male)]
        [InlineData("female", GenderTypeDTO.Female)]
        public void GetPassengersTest(string gender, GenderTypeDTO expected)
        {
            //Arrange
            var mockPersonRepository = new Mock<IRepository<Person>>();
            var mockBookingRepository = new Mock<IRepository<Booking>>();

            mockBookingRepository.Setup(repo => repo.GetAll())
                    .Returns(dataSource.Bookings);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();

            IPassengerService passsengerService = new PassengersService(mockPersonRepository.Object, mockBookingRepository.Object, mapper);

            // Act
            var response = passsengerService.GetPassengersByGender(gender);

            //Assert
            Assert.NotNull(response);
            Assert.All(response, p => Assert.Equal(expected, p.Gender));
        }
    }
}
