using Microsoft.AspNetCore.Http;
using NotesApp.Domain.Dtos;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface IAttachmentService
    {
        public Task<IEnumerable<AttachmentDto>> GetAllAsync(Guid? userId = null);
        public Task<AttachmentDto> GetByIdAsync(Guid id);
        public Task CreateAsync(IFormFile attachment);
        public Task DeleteAsync(Guid id);
    }
}
