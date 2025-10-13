using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;
using NotesApp.Dto;

namespace NotesApp.Application.FunctionalTests.Tag
{
    public class ReadTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {
        
        [Fact]
        public async Task GetAllByCurrentUser_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var expected = TestData.TestTags.Where(t => t.UserId == TestData.TestUser.Id).ToList();

            //Act
            var response = await Client.GetAsync("api/tag");
            var tags = await response.Content.ReadFromJsonAsync<IEnumerable<TagResponseDto>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(tags);
            Assert.Equal(expected.Count, tags.Count());
            Assert.True(tags.All(t => t.UserId == TestData.TestUser.Id));
        }


        [Fact]
        public async Task GetAllByCurrentUser_Unauthorized()
        {
            //Arrange

            //Act
            var response = await Client.GetAsync("api/tag");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task GetById_PersonalTag_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var expected = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUser.Id);

            //Act
            var response = await Client.GetAsync($"api/Tag/{expected?.Id}");
            var tag = await response.Content.ReadFromJsonAsync<TagResponseDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(tag);
            Assert.Equal(expected?.Id, tag.Id);
        }


        [Fact]
        public async Task GetById_AlienTag_Unauthorized()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var expected = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUserAdmin.Id);

            //Act

            //Assert
            var response = await Client.GetAsync($"api/Tag/{expected?.Id}");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task GetById_DoesNotExist()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            //Act
            var response = await Client.GetAsync($"api/Tag/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task GetById_Unauthorized()
        {
            //Arrange
            var expected = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUserAdmin.Id);

            //Assert
            var response = await Client.GetAsync($"api/Tag/{expected?.Id}");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
