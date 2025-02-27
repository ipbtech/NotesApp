using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Auth.Dto;
using NotesApp.Common.Tests;

namespace NotesApp.Auth.FunctionalTests
{
    public class ChangePasswordTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {
        [Fact]
        public async Task Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            var passwordDto = new ChangePasswordDto(TestData.TestUserPassword, "Qwerty3$!");

            //Act
            var changePasswordResponse = await Client.PostAsJsonAsync("auth/change-password", passwordDto);
            var newTokens = await GetTokensAsync(password: passwordDto.NewPassword);

            //Assert
            Assert.Equal(HttpStatusCode.OK, changePasswordResponse.StatusCode);
            Assert.False(string.IsNullOrEmpty(newTokens?.AccessToken));
            Assert.False(string.IsNullOrEmpty(newTokens.RefreshToken));
        }

        [Fact]
        public async Task Unauthorized()
        {
            //Arrange
            var passwordDto = new ChangePasswordDto(TestData.TestUserPassword, "Qwerty3$!");

            //Act
            var changePasswordResponse = await Client.PostAsJsonAsync("auth/change-password", passwordDto);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, changePasswordResponse.StatusCode);
        }

        [Fact]
        public async Task Validation()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var passwordDto = new ChangePasswordDto(TestData.TestUserPassword, "Qwerty");

            //Act
            var changePasswordResponse = await Client.PostAsJsonAsync("auth/change-password", passwordDto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, changePasswordResponse.StatusCode);
        }

        [Fact]
        public async Task OldPasswordUncorrected()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var passwordDto = new ChangePasswordDto("Her0=11W", "Qwerty3$!");

            //Act
            var changePasswordResponse = await Client.PostAsJsonAsync("auth/change-password", passwordDto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, changePasswordResponse.StatusCode);
        }

        [Fact]
        public async Task PasswordsMatched()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var passwordDto = new ChangePasswordDto(TestData.TestUserPassword, TestData.TestUserPassword);

            //Act
            var changePasswordResponse = await Client.PostAsJsonAsync("auth/change-password", passwordDto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, changePasswordResponse.StatusCode);
        }
    }
}
