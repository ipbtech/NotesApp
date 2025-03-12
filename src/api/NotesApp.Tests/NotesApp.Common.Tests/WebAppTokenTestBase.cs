using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Auth.Options;

namespace NotesApp.Common.Tests
{
    // ReSharper disable InconsistentNaming
    public class WebAppTokenTestBase(
        WebApplicationFactory<Program> factory) : WebAppTestBase(factory)
    {
        private const double ONE_SECOND_IN_MINUTE = 1 / (double)60;
        private const double THREE_SECOND_IN_DAY = 1 / (double)28800;
        
        protected override WebApplicationFactory<Program> ConfigureFactory()
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureTestServices(services =>
                {
                    services.Configure<JwtOptions>(opt =>
                    {
                        opt.AccessTokenExpirationMinutes = ONE_SECOND_IN_MINUTE;
                        opt.RefreshTokenExpirationDays = THREE_SECOND_IN_DAY;
                    });
                    MockDbService(services);
                });
            });
        }
    }
}
