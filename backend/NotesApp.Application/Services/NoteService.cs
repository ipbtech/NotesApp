using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Dtos;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Repositories;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _notesRepos;
        
        public NoteService(IRepository<Note> notesRepos)
        {
            _notesRepos = notesRepos;
        }


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
