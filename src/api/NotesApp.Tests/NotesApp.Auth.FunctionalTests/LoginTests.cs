using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;
using NotesApp.Dto;

namespace NotesApp.Auth.FunctionalTests
{
    public class LogInTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {
        
        [Fact]
        public async Task Success()
        {
            var tokenDto = await GetTokensAsync();

            //Assert
            Assert.False(string.IsNullOrEmpty(tokenDto?.AccessToken));
            Assert.False(string.IsNullOrEmpty(tokenDto.RefreshToken));
        }

        [Fact]
        public async Task UncorrectedPassword()
        {
            //Arrange
            var dto = new LoginRequestDto(TestData.TestUser.Email, "qwertYY%$!");

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
