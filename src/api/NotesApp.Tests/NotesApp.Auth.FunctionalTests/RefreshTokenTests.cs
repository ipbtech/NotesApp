using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Auth.Dto;
using NotesApp.Common.Tests;

namespace NotesApp.Auth.FunctionalTests
{
    public class RefreshTokenTests(
        WebApplicationFactory<Program> factory) : WebAppTokenTestBase(factory)
    {
        
        [Fact]
        public async Task Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            var dto = new RefreshTokenRequestDto(TestData.TestUser.Id, tokens?.RefreshToken ?? string.Empty);

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
            var dto = new RefreshTokenRequestDto(TestData.TestUser.Id, tokens?.RefreshToken ?? string.Empty);
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
            var tokens = await GetTokensAsync();
            var dto = new RefreshTokenRequestDto(TestData.TestUser.Id, "invalidToken");

            //Act
            var response = await Client.PostAsJsonAsync("/auth/refresh-token", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Validation()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            var dto = new RefreshTokenRequestDto(TestData.TestUser.Id, string.Empty);

            //Act
            var response = await Client.PostAsJsonAsync("/auth/refresh-token", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
