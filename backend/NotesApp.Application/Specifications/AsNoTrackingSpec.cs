using Ardalis.Specification;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.Application.Specifications
{
    public class AsNoTrackingSpec<TEntity> : Specification<TEntity> where TEntity : BaseEntity
    {
        public AsNoTrackingSpec()
        {
            Query.AsNoTracking();
        }
    }
}
