using NotesApp.Application.Specifications;
using NotesApp.Domain.Contracts.Repositories;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Services
{
    public class NoteService(IRepository<Note> repository, IRepository<User> userRepo)
    {

        // {3D786477-15C3-4D48-8EBA-41C99A8268FA}
        static Guid SeedUserId = Guid.Parse("3D786477-15C3-4D48-8EBA-41C99A8268FA");


        public async Task GetNotesAsync(int? userId = null)
        {
            var ents = await repository.ListAsync(new AsNoTrackingSpec<Note>());

        }

        public async Task Create()
        {
            var newEntity = new Note()
            {
                Name = "hello",
                Description = "hello",
                UserId = SeedUserId,
            };

            await repository.AddAsync(newEntity);
        }


        public async Task CreateSeedUser()
        {
            var user = new User
            {
                Id = SeedUserId,
                Email = "hello",
                UserName = "hello",
                Password = "hello",
            };

            await userRepo.AddAsync(user);
        }
    }
}
