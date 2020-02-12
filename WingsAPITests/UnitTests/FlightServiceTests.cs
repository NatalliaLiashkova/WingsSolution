using WingsOn.Dal;
using WingsOn.Domain;
using Xunit;
using AutoMapper;
using Moq;
using WingsAPI.Profiles;
using WingsOnServices;
using WingsOnServices.Interfaces;
using WingsOn.DTO;

namespace WingsAPITests.UnitTests
{
    public class FlightServiceTests
    {
        private readonly DataSource dataSource;

        public FlightServiceTests()
        {
            dataSource = new DataSource();
        }

        [Theory]
        [InlineData("PZ696")]
        public void GetPassengersByFlight(string flightNumber)
        {
            //Arrange
            var mockFlightRepository = new Mock<IRepository<Flight>>();
            var mockBookingRepository = new Mock<IRepository<Booking>>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = mockMapper.CreateMapper();
            InitializeMock(mockFlightRepository, mockBookingRepository);

            IFlightService flightService = new FlightService(mockBookingRepository.Object, mockFlightRepository.Object, mapper);

            // Act
            var response = flightService.GetPassengersByFlight(flightNumber);

            // Assert
            Assert.NotNull(response);
            Assert.All(response, p => Assert.IsType<PersonDTO>(p));
        }

        #region Private Methods
        private void InitializeMock(Mock<IRepository<Flight>> flightMock, Mock<IRepository<Booking>> bookingMock)
        {
            flightMock.Setup(repo => repo.GetAll())
                    .Returns(dataSource.Flights);

            bookingMock.Setup(repo => repo.GetAll())
                    .Returns(dataSource.Bookings);
        }
        #endregion
    }
}
