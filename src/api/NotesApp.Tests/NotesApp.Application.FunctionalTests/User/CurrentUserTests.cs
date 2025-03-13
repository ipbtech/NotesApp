using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;
using NotesApp.Dto;

namespace NotesApp.Application.FunctionalTests.User
{
    public class CurrentUserTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {


        [Fact]
        public async Task Get_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            //Act
            var response = await Client.GetAsync("api/user");
            var responseData = await response.Content.ReadFromJsonAsync<UserResponseDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseData);
            Assert.Equal(TestData.TestUserAdmin.Id, responseData.Id);
        }


        [Fact]
        public async Task Get_Unauthorized()
        {
            //Arrange

            //Act
            var response = await Client.GetAsync("api/user");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task Update_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var dto = new UserRequestDto("UserName");

            //Act
            var response = await Client.PutAsJsonAsync("api/user", dto);
            var responseData = await response.Content.ReadFromJsonAsync<UserResponseDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseData);
            Assert.Equal(dto.UserName, responseData.UserName);
        }


        [Fact]
        public async Task Update_WithEmptyName_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var dto = new UserRequestDto("");

            //Act
            var response = await Client.PutAsJsonAsync("api/user", dto);
            var responseData = await response.Content.ReadFromJsonAsync<UserResponseDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseData);
            Assert.Equal(dto.UserName, responseData.UserName);
        }


        [Fact]
        public async Task Update_Unauthorized()
        {
            //Arrange
            var dto = new UserRequestDto("UserName");

            //Act
            var response = await Client.PutAsJsonAsync("api/user", dto);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task Delete_Unauthorized()
        {
            //Arrange

            //Act
            var response = await Client.DeleteAsync("api/user");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            var loginDto = new LoginRequestDto(TestData.TestUserAdmin.Email, TestData.TestUserPassword);
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            //Act
            var deleteResponse = await Client.DeleteAsync("api/user");
            var loginResponse = await Client.PostAsJsonAsync("auth/login", loginDto);

            //Assert
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, loginResponse.StatusCode);
        }
    }
}