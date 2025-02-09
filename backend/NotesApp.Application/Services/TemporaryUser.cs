using NotesApp.Domain.Entities;

namespace NotesApp.Application.Services
{
    internal class TemporaryUser
    {
        public User User
        {
            get => new User
            {
                Id = Guid.Parse("3D786477-15C3-4D48-8EBA-41C99A8268FA"),
                UserName = "Temporary",
                Email = "hello@world.ru",
                Password = "password",
            };
        }
    }
}
