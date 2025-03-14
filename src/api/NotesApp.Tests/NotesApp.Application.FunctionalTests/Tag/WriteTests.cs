using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;
using NotesApp.Dto;

namespace NotesApp.Application.FunctionalTests.Tag
{
    public class WriteTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {
        
        [Fact]
        public async Task Create_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var newTagName = "new_tag";

            //Act
            var response = await Client.PostAsync($"api/tag/{newTagName}", null);
            var tag = await response.Content.ReadFromJsonAsync<TagResponseDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(tag);
            Assert.Equal(newTagName, tag.Name);
            Assert.Equal(TestData.TestUser.Id, tag.UserId);
        }


        [Fact]
        public async Task Create_Unauthorized()
        {
            //Arrange
            var newTagName = "new_tag";

            //Act
            var response = await Client.PostAsync($"api/tag/{newTagName}", null);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task Create_AlreadyExistByUser()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var alreadyExisted = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUser.Id);

            //Act
            var response = await Client.PostAsync($"api/tag/{alreadyExisted?.Name}", null);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task Update_PersonalTag_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var existedTag = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUser.Id);
            var newTagName = "new_tag";

            //Act
            var response = await Client.PutAsync($"api/tag/{existedTag?.Id}/{newTagName}", null);
            var tag = await response.Content.ReadFromJsonAsync<TagResponseDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(tag);
            Assert.Equal(newTagName, tag.Name);
        }


        [Fact]
        public async Task Update_PersonalTag_AlreadyExistByUser()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var existedTag = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUser.Id);

            //Act
            var response = await Client.PutAsync($"api/tag/{existedTag?.Id}/{existedTag?.Name}", null);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task Update_AlienTag_Unauthorized()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var existedTag = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUserAdmin.Id);

            //Act
            var response = await Client.PutAsync($"api/tag/{existedTag?.Id}/{existedTag?.Name}", null);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task Update_DoesNotExist()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var existedTag = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUser.Id);

            //Act
            var response = await Client.PutAsync($"api/tag/{Guid.NewGuid()}/{existedTag?.Name}", null);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task Update_Unauthorized()
        {
            //Arrange
            var existedTag = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUser.Id);

            //Act
            var response = await Client.PutAsync($"api/tag/{existedTag?.Id}/{existedTag?.Name}", null);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task Delete_PersonalTag_Success()
        {
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var existedTag = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUser.Id);

            //Act
            var delResponse = await Client.DeleteAsync($"api/tag/{existedTag?.Id}");
            var getResponse = await Client.GetAsync($"api/Tag/{existedTag?.Id}");

            //Assert
            Assert.Equal(HttpStatusCode.OK, delResponse.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }


        [Fact]
        public async Task Delete_AlienTag_Unauthorized()
        {
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);
            var existedTag = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUserAdmin.Id);

            //Act
            var response = await Client.DeleteAsync($"api/tag/{existedTag?.Id}");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task Delete_DoesNotExist()
        {
            //Arrange
            var tokens = await GetTokensAsync(TestData.TestUser.Email, TestData.TestUserPassword);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            //Act
            var response = await Client.DeleteAsync($"api/tag/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task Delete_Unauthorized()
        {
            //Arrange
            var existedTag = TestData.TestTags.FirstOrDefault(t => t.UserId == TestData.TestUser.Id);

            //Act
            var response = await Client.DeleteAsync($"api/Tag/{existedTag?.Id}");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
