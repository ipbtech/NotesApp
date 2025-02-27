using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Auth.Dto;
using NotesApp.DAL;

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
                email ?? TestData.TestUser.Email, 
                password ?? TestData.TestUserPassword);

            var response = await Client.PostAsJsonAsync("/auth/login", requestDto);
            var responseDto = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            return responseDto;
        }

        protected internal virtual WebApplicationFactory<Program> ConfigureFactory()
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureTestServices(MockDbService);
            });
        }

        protected internal void MockDbService(IServiceCollection services)
        {
            var dbContextDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(IDbContextOptionsConfiguration<NotesAppDbContext>));
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


        private void InitializeDb(NotesAppDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Users.Add(TestData.TestUser);
            dbContext.SaveChanges();
        }
    }
}