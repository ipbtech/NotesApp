using Microsoft.AspNetCore.Http;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Application.Services
{
    internal class AttachmentService : IAttachmentService
    {
        public Task CreateAsync(IFormFile attachment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Attachment>> GetAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Attachment> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
