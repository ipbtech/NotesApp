using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.DAL;
using NotesApp.Dto;

namespace NotesApp.Common.Tests
{
    public class WebAppTestBase(
        WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly SqliteConnection _dbConnection = new("DataSource=:memory:");
        private readonly WebApplicationFactoryClientOptions _clientOptions = new()
        {
            BaseAddress = new Uri("http://localhost"),
        };

        private HttpClient? _client;
        protected HttpClient Client => 
            _client ??= ConfigureFactory().CreateClient(_clientOptions);


        public void Dispose()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
        }

        protected async Task<LoginResponseDto?> GetTokensAsync(
            string? email = null,
            string? password = null)
        {
            var requestDto = new LoginRequestDto(
                email ?? TestData.TestUserAdmin.Email, 
                password ?? TestData.TestUserPassword);

            var response = await Client.PostAsJsonAsync("/auth/login", requestDto);
            var responseDto = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            return responseDto;
        }

        protected virtual WebApplicationFactory<Program> ConfigureFactory()
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureTestServices(MockDbService);
            });
        }

        protected void MockDbService(IServiceCollection services)
        {
            var dbContextDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(IDbContextOptionsConfiguration<NotesAppDbContext>));
            if (dbContextDescriptor != null) 
                services.Remove(dbContextDescriptor);

            _dbConnection.Open();
            services.AddDbContext<NotesAppDbContext>(opt =>
            {
                opt.UseSqlite(_dbConnection);
            });

            using var provider = services.BuildServiceProvider();
            var dbContext = provider.GetRequiredService<NotesAppDbContext>();
            InitializeDb(dbContext);
        }

        protected Stream CreateTestFileStream(long sizeInBytes)
        {
            var stream = new MemoryStream();
            var random = new Random();
            var buffer = new byte[1024];
            while (stream.Length < sizeInBytes)
            {
                random.NextBytes(buffer);
                stream.Write(buffer, 0, buffer.Length);
            }
            stream.Position = 0;
            return stream;
        }

        protected MultipartFormDataContent CreateMultipartFormDataContent(
            Stream stream, 
            string propertyName, 
            string fileName, 
            string mediaTypeFormat)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mediaTypeFormat);
            content.Add(fileContent, propertyName, fileName);
            return content;
        }


        private void InitializeDb(NotesAppDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Users.AddRange(TestData.TestUserAdmin, TestData.TestUser);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();

            dbContext.Tags.AddRange(TestData.TestTags);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();
        }
    }
}