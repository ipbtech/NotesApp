using NotesApp.DAL;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application.Services
{
    internal class NoteService(
        NotesAppDbContext dbContext) : INoteService
    {
        public Task<Note> CreateAsync(Note noteDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Note>> GetAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Note>> GetAllAsync(IEnumerable<Guid> tagIds, NoteSortType sortType, int page = 1, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task NotifyAsync(Guid noteId, bool onlyForMe)
        {
            throw new NotImplementedException();
        }

        public Task ShareWithAnotherUserAsync(Guid noteId, Guid anotherUserId)
        {
            throw new NotImplementedException();
        }

        public Task<Note> UpdateAsync(Guid id, Note noteDto)
        {
            throw new NotImplementedException();
        }
    }
}
