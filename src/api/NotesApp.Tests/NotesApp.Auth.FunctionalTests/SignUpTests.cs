using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;
using NotesApp.Dto;

namespace NotesApp.Auth.FunctionalTests
{
    public class SignUpTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {
        
        [Fact]
        public async Task Success()
        {
            //Arrange
            var dto = new SignUpRequestDto("newuser@user.com","Aab#$77l", "Aab#$77l");

            //Act
            var response = await Client.PostAsJsonAsync("/auth/sign-up", dto);
            var data = await response.Content.ReadFromJsonAsync<string>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(Guid.TryParse(data, out _));
            Assert.NotEqual(Guid.Empty, Guid.Parse(data));
        }


        [Fact]
        public async Task EmailAlreadyExist()
        {
            //Arrange
            var dto = new SignUpRequestDto(
                TestData.TestUser.Email, 
                "Aab#$77l", 
                "Aab#$77l");

            //Act
            var response = await Client.PostAsJsonAsync("/auth/sign-up", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task Validation()
        {
            //Arrange
            var dto = new SignUpRequestDto(
                TestData.TestUser.Email,
                "Aab",
                "Aab");

            //Act
            var response = await Client.PostAsJsonAsync("/auth/sign-up", dto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
