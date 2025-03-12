using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;
using NotesApp.Dto;

namespace NotesApp.Auth.FunctionalTests
{
    public class LogOutTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {
        
        [Fact]
        public async Task Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var logoutDto = new RefreshTokenRequestDto(TestData.TestUser.Id, tokens?.RefreshToken ?? string.Empty);

            //Act
            var response = await Client.PostAsJsonAsync("/auth/logout", logoutDto);


            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Unauthorized()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            var logoutDto = new RefreshTokenRequestDto(TestData.TestUser.Id, tokens?.RefreshToken ?? string.Empty);

            //Act
            var response = await Client.PostAsJsonAsync("/auth/logout", logoutDto);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}