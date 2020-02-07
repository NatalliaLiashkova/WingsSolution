using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http;
using WingsAPI;
using Xunit;

namespace WingsAPITests
{
    public class IntegrationTests
    {
        private readonly HttpClient client;

        public IntegrationTests()
        {
            var server = new TestServer(new WebHostBuilder().UseEnvironment("Development").UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Theory]
        [InlineData("Get", 91)]
        public void GetPersonByIdTest(string method, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"api/passengers/{id}");
            // Act
            var response = client.SendAsync(request);
            // Assert
            response.Result.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Theory]
        [InlineData("Get", "")]
        [InlineData("Get", "Male")]
        [InlineData("Get", "Female")]
        public void GetPassengersTest(string method, string gender)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"api/passengers?gender={gender}");
            // Act
            var response = client.SendAsync(request);
            // Assert
            response.Result.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Theory]
        [InlineData("Get", "PZ696")]
        public void GetPassengersByFlightTest(string method, string flight)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"api/passengers/getPassengersByFlight/{flight}");
            // Act
            var response = client.SendAsync(request);
            // Assert
            response.Result.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
        }

        #region Exception methods
        [Theory]
        [InlineData("Get", -100)]
        public void GetPersonByIdExceptionTest(string method, int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"api/passengers/{id}");
            // Act
            var response = client.SendAsync(request);
            // Assert
            Assert.Throws<HttpRequestException>(() => response.Result.EnsureSuccessStatusCode());
        }

        [Theory]
        [InlineData("Get", "")]
        public void GetPassengersByFlightExceptionTest(string method, string flight)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"api/passengers/getPassengersByFlight/{flight}");
            // Act
            var response = client.SendAsync(request);
            // Assert            
            Assert.Throws<HttpRequestException>(() => response.Result.EnsureSuccessStatusCode());
        }
        #endregion
    }
}
