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

        public async Task<Guid> RegisterAsync(SignUpRequestDto signUpRequestDto)
        {
            if (await dbContext.Users.AnyAsync(e => e.Email == signUpRequestDto.Email))
            {
                throw new ArgumentException("Email already exists");
            }
            
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

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await dbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email == loginRequestDto.Email) ??
                throw new ArgumentException("Email does not exist");

            if (!StringHasher.VerifyHash(loginRequestDto.Password, user.PasswordHash))
            {
                throw new ArgumentException("Email or password is invalid");
            }

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                ExpiredDateTimeUtc = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays),
                TokenHash = StringHasher.ToHash(refreshToken)
            };
            await dbContext.RefreshTokens.AddAsync(refreshTokenEntity);
            await dbContext.SaveChangesAsync();
            return new LoginResponseDto(accessToken, refreshToken);
        }

        public async Task LogoutAsync(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var tokens = await dbContext.RefreshTokens
                .Where(e => e.UserId == refreshTokenRequestDto.UserId).ToListAsync();

            var sessionToken = tokens.FirstOrDefault(t =>
                StringHasher.VerifyHash(refreshTokenRequestDto.RefreshToken, t.TokenHash));

            if (sessionToken is not null)
            {
                dbContext.RefreshTokens.Remove(sessionToken);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var tokens = await dbContext.RefreshTokens
                .Where(e => e.UserId == refreshTokenRequestDto.UserId).ToListAsync();

            var sessionToken = tokens.FirstOrDefault(t =>
                StringHasher.VerifyHash(refreshTokenRequestDto.RefreshToken, t.TokenHash)) ??
                throw new ArgumentException("The token is invalid or already has revoked");

            if (sessionToken.ExpiredDateTimeUtc <= DateTime.UtcNow)
            {
                dbContext.RefreshTokens.Remove(sessionToken);
                await dbContext.SaveChangesAsync();

                throw new ArgumentException("The token has expired");
            }

            var user = await dbContext.Users.FindAsync(sessionToken.UserId) ??
                    throw new Exception("Something went wrong");

            var accessToken = GenerateAccessToken(user);
            return new RefreshTokenResponseDto(accessToken);
        }

        public async Task RevokeRefreshTokenAsync(Guid? userId = null)
        {
            List<RefreshToken> tokens;
            if (userId.HasValue)
            {
                tokens = await dbContext.RefreshTokens
                    .Where(e => e.UserId == userId).ToListAsync();
            }
            else
            {
                tokens = await dbContext.RefreshTokens.ToListAsync();
            }

            dbContext.RefreshTokens.RemoveRange(tokens);
            await dbContext.SaveChangesAsync();
        }


        public async Task ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto)
        {
            var user = await dbContext.Users.FindAsync(userId) ??
                throw new ArgumentException("User doesn't exist");

            if (!StringHasher.VerifyHash(changePasswordDto.OldPassword, user.PasswordHash))
                throw new ArgumentException("Old password is uncorrected");

            if (StringHasher.VerifyHash(changePasswordDto.NewPassword, user.PasswordHash))
                throw new ArgumentException("Passwords mustn't match");

            user.PasswordHash = StringHasher.ToHash(changePasswordDto.NewPassword);
            await RevokeRefreshTokenAsync(user.Id);
            await dbContext.SaveChangesAsync();
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
