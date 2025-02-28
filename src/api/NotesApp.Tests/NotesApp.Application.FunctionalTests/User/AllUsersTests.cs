using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;
using NotesApp.Dto;

namespace NotesApp.Application.FunctionalTests.User
{
    public class AllUsersTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {

        [Fact]
        public async Task AllUsersSuccess()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            //Act
            var response = await Client.GetAsync("api/user/all");
            var responseData = await response.Content.ReadFromJsonAsync<IEnumerable<UserRequestDto>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseData);
            Assert.NotEmpty(responseData);
        }


        [Fact]
        public async Task AllUsersUnauthorized()
        {
            //Arrange

            //Act
            var response = await Client.GetAsync("api/user/all");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task AllUsersForbidden()
        {
            //Arrange
            var newUserSignUpDto = new SignUpRequestDto("newuser@user.com", "Aab#$77l", "Aab#$77l");
            _ = await Client.PostAsJsonAsync("/auth/sign-up", newUserSignUpDto);

            var tokens = await GetTokensAsync(newUserSignUpDto.Email, newUserSignUpDto.Password);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            //Act
            var response = await Client.GetAsync("api/user/all");

            //Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
