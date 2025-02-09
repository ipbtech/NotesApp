using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Interfaces.Mapper
{
    public interface IMapper<TEntity, TDto> 
        where TEntity : IModelId, IAuditable
        where TDto : class
    {
        public TDto MapTo(TEntity entity);
        public TEntity MapFrom(TDto dto);

        public IEnumerable<TDto> MapTo(IEnumerable<TEntity> entityCollection)
        {
            foreach (var entity in entityCollection)
            {
                yield return MapTo(entity);
            }
        }

        public IEnumerable<TEntity> MapFrom(IEnumerable<TDto> dtoCollection)
        {
            foreach(var dto in dtoCollection)
            {
                yield return MapFrom(dto);
            }
        }
    }
}
