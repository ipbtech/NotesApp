using NotesApp.Application.Specifications;
using NotesApp.Domain.Dtos;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Interfaces.Repositories;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application.Services
{
    internal class NoteService(
        IRepository<Note> repository, 
        IRepository<User> userRepo) : INoteService
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

        public Task<IEnumerable<NoteGetDto>> GetAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NoteGetDto>> GetAllAsync(IEnumerable<Guid> tagIds, NoteSortType sortType, int page = 1, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<NoteGetDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<NoteCreateDto> CreateAsync(NoteCreateDto noteDto)
        {
            throw new NotImplementedException();
        }

        public Task<NoteGetDto> UpdateAsync(Guid id, NoteUpdateDto noteDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task ShareWithAnotherUserAsync(Guid noteId, Guid anotherUserId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyAsync(Guid noteId, bool onlyForMe)
        {
            throw new NotImplementedException();
        }
    }
}
