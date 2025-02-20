using Ardalis.Specification;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Application.Specifications
{
    public class AsNoTrackingSpec<TEntity> : Specification<TEntity> where TEntity : class
    {
        public AsNoTrackingSpec()
        {
            Query.AsNoTracking();
        }
    }

    public class AsNoTrackingAndByIdSpec<TEntity> : Specification<TEntity> where TEntity : class, IEntityId
    {
        public AsNoTrackingAndByIdSpec(Guid id)
        {
            Query.AsNoTracking().Where(e => e.Id == id);
        }
    }

    public class ByUserIdSpec<TEntity> : Specification<TEntity> where TEntity : class, IUserSpecific
    {
        public ByUserIdSpec(Guid userId, bool asNoTracking = false)
        {
            if (asNoTracking)
                Query.AsNoTracking();

            Query.Where(x => x.UserId == userId);
        }
    }
}
