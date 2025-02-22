using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Application.Specifications;
using NotesApp.Auth.Dto;
using NotesApp.Auth.Options;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Interfaces.DAL;
using NotesApp.Domain.Utils;

namespace NotesApp.Auth
{
    public class AuthService(
        IOptions<JwtOptions> jwtOptions,
        IRepository<User> userRepository,
        IRepository<RefreshToken> tokenRepository)
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public async Task<Guid> RegisterAsync(
            SignUpRequestDto signUpRequestDto)
        {
            if (await userRepository.AnyAsync(new ByUserEmailSpec(signUpRequestDto.Email)))
                throw new ArgumentException("Email is already exist");
            
            var user = new User
            {
                Email = signUpRequestDto.Email,
                PasswordHash = StringHasher.ToHash(signUpRequestDto.Password),
                Role = UserRole.User,
            };

            await userRepository.AddAsync(user);
            return user.Id;
        }

        public async Task<LoginResponseDto> LoginAsync(
            LoginRequestDto loginRequestDto,
            Guid deviceSessionId)
        {
            var user = await userRepository.FirstOrDefaultAsync(new ByUserEmailSpec(loginRequestDto.Email)) ??
                throw new ArgumentException("Email is not exist");

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken()
            {
                UserId = user.Id,
                DeviceSessionId = deviceSessionId,
                ExpiredDateTimeUtc = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays),
                TokenHash = StringHasher.ToHash(refreshToken)
            };
            await tokenRepository.AddAsync(refreshTokenEntity);
            return new LoginResponseDto(accessToken, refreshToken);
        }

        public async Task LogoutAsync(Guid deviceSessionId)
        {
            var refreshToken = await tokenRepository
                .FirstOrDefaultAsync(new ByDeviceSessionIdSpec(deviceSessionId));

            if (refreshToken is not null)
            {
                await tokenRepository.DeleteAsync(refreshToken);
            }
        }

        public async Task<RefreshTokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            throw new NotImplementedException();
        }

        public async Task RevokeRefreshTokenAsync(Guid? userId)
        {
            throw new NotImplementedException();
        }
        //4b22eac4-a83c-4085-88f6-c7ff5c7121df
        //7b25e100-bd37-4f4d-9ae8-712502dea279


        private string GenerateAccessToken(User user)
        {
            var claims = new List<Claim> 
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpirationMinutes),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
