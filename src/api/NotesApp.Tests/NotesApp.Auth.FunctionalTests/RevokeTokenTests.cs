using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Auth.Dto;
using NotesApp.Common.Tests;

namespace NotesApp.Auth.FunctionalTests
{
    public class RevokeTokenTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {
        
        [Fact]
        public async Task Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            //Act
            var revokeResponse = await Client.PostAsync("auth/revoke-token/current-user", null);

            //Assert
            Assert.Equal(HttpStatusCode.OK, revokeResponse.StatusCode);
        }

        [Fact]
        public async Task Unauthorized()
        {
            //Arrange

            //Act
            var revokeResponse = await Client.PostAsync("auth/revoke-token/current-user", null);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, revokeResponse.StatusCode);
        }

        [Fact]
        public async Task ForbiddenForAllUsers()
        {
            //Arrange
            var newUserSignUpDto = new SignUpRequestDto("newuser@user.com", "Aab#$77l", "Aab#$77l");
            var signUpResponse = await Client.PostAsJsonAsync("/auth/sign-up", newUserSignUpDto);

            var tokens = await GetTokensAsync(newUserSignUpDto.Email, newUserSignUpDto.Password);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            //Act
            var revokeResponse = await Client.PostAsync("auth/revoke-token/all-users", null);

            //Assert
            Assert.Equal(HttpStatusCode.Forbidden, revokeResponse.StatusCode);
        }
    }
}
