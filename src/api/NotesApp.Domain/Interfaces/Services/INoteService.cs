using NotesApp.Dto;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface INoteService
    {
        public Task<IEnumerable<NoteResponseDto>> GetAsync(NotePaginationDto? paginationDto, Guid currentUserId);
        public Task<NoteResponseDto> GetAsync(Guid id, Guid currentUserId);
        public Task<NoteResponseDto> CreateAsync(NoteRequestDto noteDto, Guid currentUserId);
        public Task<NoteResponseDto> UpdateAsync(Guid id, NoteRequestDto noteDto, Guid currentUserId);
        public Task<NoteResponseDto> UpdateTagAsync(NoteTagUpdatingDto tagUpdatingDto, Guid currentUserId);
        public Task DeleteAsync(Guid id, Guid currentUserId);
        public Task NotifyAsync(NoteNotificationDto notificationDto, Guid currentUserId);
    }
}
