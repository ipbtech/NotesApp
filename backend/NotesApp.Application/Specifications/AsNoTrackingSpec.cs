using Ardalis.Specification;

namespace NotesApp.Application.Specifications
{
    internal class AsNoTrackingSpec<TEntity> : Specification<TEntity> where TEntity : class
    {
        public AsNoTrackingSpec()
        {
            Query.AsNoTracking();
        }
    }
}
