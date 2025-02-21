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
        ITransactionManager transactionManager,
        IRepository<User> userRepository,
        IRepository<RefreshToken> tokenRepository)
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public async Task<Guid> RegisterAsync(SignUpDto signUpDto)
        {
            if (await userRepository.AnyAsync(new ByUserEmailSpec(signUpDto.Email)))
                throw new ArgumentException("Email is already exist");
            
            var user = new User
            {
                Email = signUpDto.Email,
                PasswordHash = StringHasher.ToHash(signUpDto.Password),
                Role = UserRole.User,
            };

            await userRepository.AddAsync(user);
            return user.Id;
        }

        public async Task<TokensDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userRepository.FirstOrDefaultAsync(new ByUserEmailSpec(loginDto.Email)) ??
                throw new ArgumentException("Email is not exist");

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken()
            {
                UserId = user.Id,
                ExpiredDateTimeUtc = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays),
                TokenHash = StringHasher.ToHash(refreshToken)
            };
            await tokenRepository.AddAsync(refreshTokenEntity);
            return new TokensDto(accessToken, refreshToken);
        }

        public async Task LogoutAsync(RefreshTokenDto refreshTokenDto)
        {
            throw new NotImplementedException();
        }

        public async Task<RefreshTokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            throw new NotImplementedException();
        }

        public async Task RevokeRefreshTokenAsync(Guid? userId)
        {
            throw new NotImplementedException();
        }


        private string GenerateAccessToken(User user)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
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
