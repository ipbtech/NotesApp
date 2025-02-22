using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Auth.Dto;
using NotesApp.Auth.Options;
using NotesApp.DAL;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Utils;

namespace NotesApp.Auth
{
    public class AuthService(
        IOptions<JwtOptions> jwtOptions,
        NotesAppDbContext dbContext)
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public async Task<Guid> RegisterAsync(
            SignUpRequestDto signUpRequestDto)
        {
            if (await dbContext.Users.AnyAsync(e => e.Email == signUpRequestDto.Email))
                throw new ArgumentException("Email already exists");
            
            var user = new User
            {
                Email = signUpRequestDto.Email,
                PasswordHash = StringHasher.ToHash(signUpRequestDto.Password),
                Role = UserRole.User,
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<LoginResponseDto> LoginAsync(
            LoginRequestDto loginRequestDto,
            Guid deviceSessionId)
        {
            var user = await dbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email == loginRequestDto.Email) ??
                    throw new ArgumentException("Email does not exist");

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                DeviceSessionId = deviceSessionId,
                ExpiredDateTimeUtc = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays),
                TokenHash = StringHasher.ToHash(refreshToken)
            };
            await dbContext.RefreshTokens.AddAsync(refreshTokenEntity);
            await dbContext.SaveChangesAsync();
            return new LoginResponseDto(accessToken, refreshToken);
        }

        public async Task LogoutAsync(
            Guid userId,
            Guid deviceSessionId)
        {
            var refreshToken = await dbContext.RefreshTokens
                .FirstOrDefaultAsync(e => e.UserId == userId && e.DeviceSessionId == deviceSessionId);

            if (refreshToken is not null)
            {
                dbContext.RefreshTokens.Remove(refreshToken);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(
            RefreshTokenRequestDto refreshTokenRequestDto,
            Guid deviceSessionId)
        {
            var token = await dbContext.RefreshTokens
                .Include(e => e.User)
                .FirstOrDefaultAsync(e =>
                    e.UserId == refreshTokenRequestDto.UserId && e.DeviceSessionId == deviceSessionId);

            if (token is null || !StringHasher.VerifyHash(refreshTokenRequestDto.RefreshToken, token.TokenHash))
            {
                throw new ArgumentException("The token is invalid or already has revoked");
            }

            if (token.ExpiredDateTimeUtc <= DateTime.UtcNow)
            {
                dbContext.RefreshTokens.Remove(token);
                await dbContext.SaveChangesAsync();

                throw new ArgumentException("The token has expired");
            }

            var accessToken = GenerateAccessToken(token.User);
            return new RefreshTokenResponseDto(accessToken);
        }

        //TODO create revokable mechanism 
        public async Task RevokeRefreshTokenAsync(Guid? userId)
        {
            throw new NotImplementedException();
        }


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
