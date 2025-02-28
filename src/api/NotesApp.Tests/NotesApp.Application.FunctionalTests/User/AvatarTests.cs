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

            //Act

            //Assert
        }


        [Fact]
        public async Task Get_Success()
        {
            //Arrange

            //Act

            //Assert
        }


        [Fact]
        public async Task Get_Unauthorized()
        {
            //Arrange

            //Act

            //Assert
        }


        [Fact]
        public async Task Upload_Unauthorized()
        {
            //Arrange

            //Act

            //Assert
        }


        [Fact]
        public async Task UploadPNG_Success()
        {
            //Arrange

            //Act

            //Assert
        }


        [Fact]
        public async Task UploadJPG_Success()
        {
            //Arrange

            //Act

            //Assert
        }


        [Fact]
        public async Task Upload_FileFormatUncorrected()
        {
            //Arrange

            //Act

            //Assert
        }


        [Fact]
        public async Task Upload_FileSizeIsTooMuch()
        {
            //Arrange

            //Act

            //Assert
        }


        [Fact]
        public async Task Delete_Success()
        {
            //Arrange

            //Act

            //Assert
        }


        [Fact]
        public async Task Delete_Unauthorized()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
