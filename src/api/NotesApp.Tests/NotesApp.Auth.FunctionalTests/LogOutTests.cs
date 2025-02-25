using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Auth.Dto;
using NotesApp.Auth.FunctionalTests.Base;

namespace NotesApp.Auth.FunctionalTests
{
    public class LogOutTests(
        WebApplicationFactory<Program> factory) : AuthTestBase(factory)
    {
        
        [Fact]
        public async Task Success()
        {
            //Arrange
            var loginDto = new LoginRequestDto(AuthTestUser.TestUser.Email, AuthTestUser.TestUserPassword);

            var loginResponse = await Client.PostAsJsonAsync("/auth/login", loginDto);
            var data = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", data?.AccessToken);
            var logoutDto = new RefreshTokenRequestDto(AuthTestUser.TestUser.Id, data?.RefreshToken ?? string.Empty);

            //Act
            var logoutResponse = await Client.PostAsJsonAsync("/auth/logout", logoutDto);


            //Assert
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            Assert.False(string.IsNullOrEmpty(data?.AccessToken));
            Assert.False(string.IsNullOrEmpty(data?.RefreshToken));

            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        }

        [Fact]
        public async Task Unauthorized()
        {
            //Arrange
            var loginDto = new LoginRequestDto(AuthTestUser.TestUser.Email, AuthTestUser.TestUserPassword);

            var loginResponse = await Client.PostAsJsonAsync("/auth/login", loginDto);
            var data = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();

            var logoutDto = new RefreshTokenRequestDto(AuthTestUser.TestUser.Id, data?.RefreshToken ?? string.Empty);

            //Act
            var logoutResponse = await Client.PostAsJsonAsync("/auth/logout", logoutDto);


            //Assert
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            Assert.Equal(HttpStatusCode.Unauthorized, logoutResponse.StatusCode);
        }
    }
}