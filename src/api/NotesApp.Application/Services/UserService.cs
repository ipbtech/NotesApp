using Microsoft.EntityFrameworkCore;
using NotesApp.DAL;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Domain.Interfaces.Services;
using NotesApp.Dto;

namespace NotesApp.Application.Services
{
    internal class UserService(
        NotesAppDbContext dbContext,
        IMapper<User, UserResponseDto> mapper) : IUserService
    {

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            return await dbContext.Users
                .Select(u => 
                    new UserResponseDto(u.Id, u.Email, u.UserName, u.Role.ToString()))
                .ToListAsync();
        }

        public Task<UserResponseDto?> GetAsync(Guid id)
        {
            return dbContext.Users
                .Where(u => u.Id == id)
                .Select(u => new UserResponseDto(u.Id, u.Email, u.UserName, u.Role.ToString()))
                .FirstOrDefaultAsync();
        }

        public async Task<UserResponseDto> UpdateAsync(Guid id, UserRequestDto newUserInfo)
        {
            var user = await dbContext.Users.FindAsync(id) ??
                throw new NullReferenceException("User does not exist");

            user.UserName = newUserInfo.UserName;
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();

            return mapper.MapToDto(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await dbContext.Users.FindAsync(id) ??
                throw new NullReferenceException("User does not exist");

            //TODO delete refresh tokens

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }
}