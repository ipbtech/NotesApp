using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Interfaces.Mapping
{
    public interface IMapper<TEntity, TDto> 
        where TEntity : class, IEntityId, IAuditable
        where TDto : class
    {
        public TDto MapToDto(TEntity entity);
        public TEntity MapFromDto(TDto dto);

        public IEnumerable<TDto> MapToDto(IEnumerable<TEntity> entityCollection)
        {
            foreach (var entity in entityCollection)
            {
                yield return MapToDto(entity);
            }
        }

        public IEnumerable<TEntity> MapFromDto(IEnumerable<TDto> dtoCollection)
        {
            foreach(var dto in dtoCollection)
            {
                yield return MapFromDto(dto);
            }
        }
    }
}
