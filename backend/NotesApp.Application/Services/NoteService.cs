using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application.Services
{
    public class NoteService : INoteService
    {



        public async Task GetNotesAsync(int? userId = null)
        {
            //IQueryable<Note> query = _notesRepos.GetAll();
            //if (userId is not null)
            //    query = query.Where(note => note.UserId == userId);

            //return await query.Select(note => new NoteDto
            //{
            //    Name = note.Name,
            //    Description = note.Description,
            //    CreatedDate = note.CreatedAtUtc.ToLongDateString()
            //}).ToListAsync();
        }
    }
}
