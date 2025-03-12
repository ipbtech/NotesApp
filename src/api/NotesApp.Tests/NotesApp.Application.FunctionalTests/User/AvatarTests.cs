using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Common.Tests;

namespace NotesApp.Application.FunctionalTests.User
{
    public class AvatarTests(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {

        [Fact]
        public async Task Get_NoContent()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            //Act
            var response = await Client.GetAsync("api/user/avatar");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Fact]
        public async Task Get_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            await using var fileStream = CreateTestFileStream(1 * 1024 * 1024); // 1 mB
            var formData = CreateMultipartFormDataContent(fileStream, "image", "image.png", "image/png"); 
            await Client.PostAsync("api/user/avatar", formData);

            //Act
            var response = await Client.GetAsync("api/user/avatar");
            var contentType = response.Content.Headers.ContentType?.MediaType;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("image/png", contentType);
        }


        [Fact]
        public async Task Get_Unauthorized()
        {
            //Arrange

            //Act
            var response = await Client.GetAsync("api/user/avatar");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task Upload_Unauthorized()
        {
            //Arrange

            //Act
            var response = await Client.PostAsync("api/user/avatar", null);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task UploadPNG_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            await using var fileStream = CreateTestFileStream(1 * 1024 * 1024); // 1 mB
            var formData = CreateMultipartFormDataContent(fileStream, "image","image.png", "image/png");

            //Act
            var response = await Client.PostAsync("api/user/avatar", formData);
            var contentType = response.Content.Headers.ContentType?.MediaType;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("image/png", contentType);
        }   


        [Fact]
        public async Task UploadJPG_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            await using var fileStream = CreateTestFileStream(1 * 1024 * 1024); // 1 mB
            var formData = CreateMultipartFormDataContent(fileStream, "image", "image.jpg", "image/jpg");

            //Act
            var response = await Client.PostAsync("api/user/avatar", formData);
            var contentType = response.Content.Headers.ContentType?.MediaType;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("image/jpg", contentType);
        }


        [Fact]
        public async Task Upload_FileFormatUncorrected()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            await using var fileStream = CreateTestFileStream(1 * 1024 * 1024 + 1); // 1 mB
            var formData = CreateMultipartFormDataContent(fileStream, "image", "test.pdf", "application/pdf");

            //Act
            var response = await Client.PostAsync("api/user/avatar", formData);

            //Assert
            Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        }


        [Fact]
        public async Task Upload_FileSizeIsTooMuch()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            await using var fileStream = CreateTestFileStream(1 * 1024 * 1024 + 1); // 1 mB
            var formData = CreateMultipartFormDataContent(fileStream, "image", "image.jpg", "image/jpg");

            //Act
            var response = await Client.PostAsync("api/user/avatar", formData);

            //Assert
            Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        }


        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            var tokens = await GetTokensAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken);

            await using var fileStream = CreateTestFileStream(1 * 1024 * 1024); // 1 mB
            var formData = CreateMultipartFormDataContent(fileStream, "image", "image.png", "image/png");
            await Client.PostAsync("api/user/avatar", formData);

            //Act
            var response = await Client.DeleteAsync("api/user/avatar");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Delete_Unauthorized()
        {
            //Arrange

            //Act
            var response = await Client.DeleteAsync("api/user/avatar");

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
