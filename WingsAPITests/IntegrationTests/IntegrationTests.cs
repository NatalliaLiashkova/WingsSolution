using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using WingsAPI;
using WingsOn.Domain;
using Xunit;

namespace WingsAPITests.IntegrationTests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        #region Properties
        private readonly WebApplicationFactory<Startup> factory;
        #endregion

        #region Ctors
        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }
        #endregion

        #region Tests for HttpGet Methods
        [Theory]
        [InlineData(91)]
        public void GetPersonByIdTest(int id)
        {
            //Arrange
            var client = factory.CreateClient();

            // Act
            var response = client.GetAsync($"api/person/{id}");
            var stringResponse = response.Result.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(stringResponse.Result);

            // Assert
            Assert.Equal("Kendall Velazquez", person.Name);
        }

        [Theory]
        [InlineData("Male", 3)]
        [InlineData("Female", 5)]
        public void GetPassengersTest(string gender, int expected)
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = client.GetAsync($"api/passengers?gender={gender}");
            var stringResponse = response.Result.Content.ReadAsStringAsync();
            var persons = JsonConvert.DeserializeObject<List<Person>>(stringResponse.Result);

            // Assert
            Assert.Equal(expected, persons.Count);
        }

        [Theory]
        [InlineData("PZ696", 5)]
        public void GetPassengersByFlightTest(string flight, int expected)
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = client.GetAsync($"api/flight/getPassengers/{flight}");
            var stringResponse = response.Result.Content.ReadAsStringAsync();
            var passengers = JsonConvert.DeserializeObject<List<Person>>(stringResponse.Result);

            // Assert
            Assert.Equal(expected, passengers.Count);
        }
        #endregion

        #region Tests for HttpPatch Methods
        [Theory]
        [InlineData(91, "new address")]
        public void UpdatePassengerAddressTest(int id, string address)
        {
            // Arrange
            var client = factory.CreateClient();
            var content = new StringContent($"{{'address':'{address}'}}", Encoding.UTF8, "application/json");

            // Act
            var response = client.PatchAsync($"api/passengers/update/{id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.Result.StatusCode);
        }
        #endregion
        
        #region Exception methods
        [Theory]
        [InlineData(-100)]
        public void GetPersonByIdExceptionTest(int id)
        {
            //Arrange
            var client = factory.CreateClient();

            // Act
            var response = client.GetAsync($"api/person/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.Result.StatusCode);
        }

        [Theory]
        [InlineData("")]
        public void GetPassengersByFlightExceptionTest(string flightNumber)
        {
            //Arrange
            var client = factory.CreateClient(); 

            // Act
            var response = client.GetAsync($"api/flight/getPassengers/{flightNumber}");

            // Assert            
            Assert.Equal(HttpStatusCode.NotFound, response.Result.StatusCode);
        }
        #endregion
    }
}
