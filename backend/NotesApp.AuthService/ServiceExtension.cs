using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace NotesApp.AuthService
{
    public static class ServiceExtension
    {
        public static void AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtOpt = configuration.GetSection("JwtOptions").Get<JwtOptions>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOpt?.Issuer,
                        ValidAudience = jwtOpt?.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt?.Key))
                    };
                });


            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.OptionName));
            services.AddScoped<AuthService>();
        }
    }
}
