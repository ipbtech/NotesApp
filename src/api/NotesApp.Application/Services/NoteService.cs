using NotesApp.DAL;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Domain.Interfaces.Services;
using NotesApp.Dto;

namespace NotesApp.Application.Services
{
    internal class NoteService(
        NotesAppDbContext dbContext,
        IMapper<Note, NoteResponseDto> responseMapper,
        IMapper<Note, NoteRequestDto> requestMapper) : INoteService
    {

        public Task<IEnumerable<NoteResponseDto>> GetAsync(NotePaginationDto? paginationDto, Guid currentUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<NoteResponseDto> GetAsync(Guid id, Guid currentUserId)
        {
            var note = await dbContext.Notes.FindAsync(id) ??
                       throw new NullReferenceException("Note does not exist");

            if (note.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }

            return responseMapper.MapToDto(note);
        }


        public async Task<NoteResponseDto> CreateAsync(NoteRequestDto noteDto, Guid currentUserId)
        {
            var note = requestMapper.MapFromDto(noteDto);
            note.UserId = currentUserId;

            await dbContext.Notes.AddAsync(note);
            await dbContext.SaveChangesAsync();

            return responseMapper.MapToDto(note);
        }


        public async Task<NoteResponseDto> UpdateAsync(Guid id, NoteRequestDto noteDto, Guid currentUserId)
        {
            var note = await dbContext.Notes.FindAsync(id) ??
                       throw new NullReferenceException("Note does not exist");

            var tag = await dbContext.Tags.FindAsync(noteDto.TagId) ?? 
                      throw new NullReferenceException("Tag does not exist");

            if (note.UserId != currentUserId || tag.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }

            requestMapper.UpdateEntity(noteDto, note);
            dbContext.Notes.Update(note);
            await dbContext.SaveChangesAsync();

            return responseMapper.MapToDto(note);
        }


        public async Task<NoteResponseDto> UpdateTagAsync(NoteTagUpdatingDto tagUpdatingDto, Guid currentUserId)
        {
            var note = await dbContext.Notes.FindAsync(tagUpdatingDto.NoteId) ??
                       throw new NullReferenceException("Note does not exist");

            var tag = await dbContext.Tags.FindAsync(tagUpdatingDto.TagId) ??
                      throw new NullReferenceException("Tag does not exist");

            if (note.UserId != currentUserId || tag.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }

            note.TagId = tag.Id;
            dbContext.Notes.Update(note);
            await dbContext.SaveChangesAsync();

            return responseMapper.MapToDto(note);
        }


        public async Task DeleteAsync(Guid id, Guid currentUserId)
        {
            var note = await dbContext.Notes.FindAsync(id) ??
                       throw new NullReferenceException("Note does not exist");

            if (note.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }

            dbContext.Notes.Remove(note);
            await dbContext.SaveChangesAsync();
        }


        public Task NotifyAsync(NoteNotificationDto notificationDto, Guid currentUserId)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
