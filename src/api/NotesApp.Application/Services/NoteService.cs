using NotesApp.DAL;
using NotesApp.Domain.Interfaces.Services;
using NotesApp.Dto;

namespace NotesApp.Application.Services
{
    internal class NoteService(
        NotesAppDbContext dbContext) : INoteService
    {
        public Task<NoteResponseDto> CreateAsync(NoteRequestDto noteDto, Guid currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, Guid currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NoteResponseDto>> GetAsync(NotePaginationDto? paginationDto, Guid currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<NoteResponseDto> GetAsync(Guid id, Guid currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyAsync(NoteNotificationDto notificationDto, Guid currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<NoteResponseDto> UpdateAsync(Guid id, NoteRequestDto noteDto, Guid currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<NoteResponseDto> UpdateTagAsync(NoteTagUpdatingDto tagUpdatingDto, Guid currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
