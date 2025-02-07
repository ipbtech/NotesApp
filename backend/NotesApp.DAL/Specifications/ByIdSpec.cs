using Ardalis.Specification;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.DAL.Specifications
{
    public class ByIdSpec<TEntity> : Specification<TEntity> where TEntity : BaseEntity
    {
        ByIdSpec(Guid id) 
        {
            Query.Where(x => x.Id == id);
        }
    }
}
