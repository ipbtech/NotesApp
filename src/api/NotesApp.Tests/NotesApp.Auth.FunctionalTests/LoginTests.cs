using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Auth.Dto;
using NotesApp.Auth.FunctionalTests.Base;

namespace NotesApp.Auth.FunctionalTests
{
    public class LogInTests(
        WebApplicationFactory<Program> factory) : AuthTestBase(factory)
    {
        
        [Fact]
        public async Task Success()
        {
            //Arrange
            var dto = new LoginRequestDto(AuthTestUser.TestUser.Email, AuthTestUser.TestUserPassword);

            //Act
            var response = await Client.PostAsJsonAsync("/auth/login", dto);
            var data = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(string.IsNullOrEmpty(data?.AccessToken));
            Assert.False(string.IsNullOrEmpty(data?.RefreshToken));
        }

        [Fact]
        public async Task UncorrectedPassword()
        {
            //Arrange
            //var dto = new LoginRequestDto("newuser@user.com","Aab#$77l");
            var dto = new LoginRequestDto(AuthTestUser.TestUser.Email, "qwertYY%$!");

            //Act
            var response = await Client.PostAsJsonAsync("/auth/login", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UserDoesNotExist()
        {
            //Arrange
            var dto = new LoginRequestDto("newuser@user.com","Aab#$77l");

            //Act
            var response = await Client.PostAsJsonAsync("/auth/login", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task Validation()
        {
            //Arrange
            var dto = new LoginRequestDto("user.com", "Aab#$77l");

            //Act
            var response = await Client.PostAsJsonAsync("/auth/login", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
