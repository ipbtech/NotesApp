using Ardalis.Specification;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.DAL.Specifications
{
    public class AsNoTrackingSpec<TEntity> : Specification<TEntity> where TEntity : BaseEntity
    {
        public AsNoTrackingSpec()
        {
            Query.AsNoTracking();
        }
    }
}
