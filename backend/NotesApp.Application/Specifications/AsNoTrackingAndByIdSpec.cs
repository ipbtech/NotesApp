using Ardalis.Specification;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Application.Specifications
{
    internal class AsNoTrackingAndByIdSpec<TEntity> : Specification<TEntity> where TEntity : class, IEntityId
    {
        public AsNoTrackingAndByIdSpec(Guid id)
        {
            Query.AsNoTracking().Where(e => e.Id == id);
        }
    }
}
