using Ardalis.Specification;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Application.Specifications
{
    internal class ByUserIdSpec<TEntity> : Specification<TEntity> where TEntity : class, IUserSpecific
    {
        public ByUserIdSpec(Guid userId, bool asNoTracking = false)
        {
            if (asNoTracking)
                Query.AsNoTracking();
            
            Query.Where(x => x.UserId == userId);
        }
    }
}
