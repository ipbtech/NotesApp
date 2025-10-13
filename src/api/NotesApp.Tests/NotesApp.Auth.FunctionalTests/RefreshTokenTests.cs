using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;
using NotesApp.Dto;

namespace NotesApp.Auth.FunctionalTests
{
    public class RefreshTokenTests(
        WebApplicationFactory<Program> factory) : WebAppShortTokenTestBase(factory)
    {
        
        [Fact]
        public async Task Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            var dto = new RefreshTokenRequestDto(TestData.TestUserAdmin.Id, tokens?.RefreshToken ?? string.Empty);

            //Act
            var response = await Client.PostAsJsonAsync("/auth/refresh-token", dto);
            var data = await response.Content.ReadFromJsonAsync<RefreshTokenResponseDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(string.IsNullOrEmpty(data?.AccessToken));
        }

        [Fact]
        public async Task HasExpired()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            var dto = new RefreshTokenRequestDto(TestData.TestUserAdmin.Id, tokens?.RefreshToken ?? string.Empty);
            await Task.Delay(TimeSpan.FromSeconds(3)); //await when refresh token will expire

            //Act
            var response = await Client.PostAsJsonAsync("/auth/refresh-token", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task InvalidToken()
        {
            //Arrange
            var dto = new RefreshTokenRequestDto(TestData.TestUserAdmin.Id, "invalidToken");

            //Act
            var response = await Client.PostAsJsonAsync("/auth/refresh-token", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Validation()
        {
            //Arrange
            var dto = new RefreshTokenRequestDto(TestData.TestUserAdmin.Id, string.Empty);

            //Act
            var response = await Client.PostAsJsonAsync("/auth/refresh-token", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
