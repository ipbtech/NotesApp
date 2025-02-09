using Microsoft.AspNetCore.Http;
using NotesApp.Domain.Entities;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface IAttachmentService
    {
        public Task<IEnumerable<Attachment>> GetAllAsync(Guid? userId = null);
        public Task<Attachment> GetByIdAsync(Guid id);
        public Task CreateAsync(IFormFile attachment);
        public Task DeleteAsync(Guid id);
    }
}
