using NotesApp.DAL.Specifications;
using NotesApp.Domain.Contracts.Repositories;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Services
{
    public class NoteService(IRepository<Note> repository)
    {



        public async Task GetNotesAsync(int? userId = null)
        {
            var ents = await repository.ListAsync(new AsNoTrackingSpec<Note>());

        }
    }
}
