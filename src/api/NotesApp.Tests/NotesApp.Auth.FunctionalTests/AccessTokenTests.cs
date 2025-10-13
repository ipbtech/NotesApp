using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;

namespace NotesApp.Auth.FunctionalTests
{
    public class AccessTokenTests(
        WebApplicationFactory<Program> factory) : WebAppShortTokenTestBase(factory)
    {
        
        [Fact]
        public async Task HasExpired()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            await Task.Delay(TimeSpan.FromSeconds(3)); // await when access token will expire

            //Act
            // any authorize end-point
            var response = await Client.PostAsync("auth/revoke-token/current-user", null);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task InvalidToken()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.RefreshToken);

            //Act
            // any authorize end-point
            var response = await Client.PostAsync("auth/revoke-token/current-user", null);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
