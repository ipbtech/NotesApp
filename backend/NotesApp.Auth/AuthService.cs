using Microsoft.Extensions.Options;
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
            throw new NotImplementedException();
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
    }
}
