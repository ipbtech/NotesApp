using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Auth.Options;

namespace NotesApp.Auth
{
    public static class ServiceExtension
    {
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtOpt = configuration.GetSection(JwtOptions.OptionName).Get<JwtOptions>();
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
            services.AddAuthorization();

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.OptionName));
            services.AddScoped<AuthService>();
        }
    }
}
