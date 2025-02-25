using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotesApp.DAL;

namespace NotesApp.Auth.FunctionalTests.Base
{
    public class AuthTestBase(
        WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly SqliteConnection _dbConnection = new SqliteConnection("DataSource=:memory:");
        private readonly WebApplicationFactoryClientOptions _clientOptions = new WebApplicationFactoryClientOptions()
        {
            BaseAddress = new Uri("http://localhost"),
        };

        private HttpClient? _client;
        protected HttpClient Client => factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureTestServices(services =>
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
            });
        }).CreateClient(_clientOptions);

        public void Dispose()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
        }

        private void InitializeDb(NotesAppDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Users.Add(AuthTestUser.TestUser);
            dbContext.SaveChanges();
        }
    }
}