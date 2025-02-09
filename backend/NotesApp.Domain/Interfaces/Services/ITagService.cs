using NotesApp.Domain.Dtos;

namespace NotesApp.Domain.Interfaces.Services
{
    public interface ITagService
    {
        public Task<IEnumerable<TagGetDto>> GetAllAsync(Guid? userId = null);
        public Task<TagGetDto> GetByIdAsync(Guid id);
        public Task<TagGetDto> CreateAsync(TagCreateOrUpdateDto tagDto);
        public Task<TagGetDto> UpdateAsync(Guid id, TagCreateOrUpdateDto tagDto);
        public Task DeleteAsync(Guid id);
    }
}
