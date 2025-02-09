using Microsoft.AspNetCore.Http;
using NotesApp.Domain.Dtos;
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

        public Task<IEnumerable<AttachmentDto>> GetAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<AttachmentDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
