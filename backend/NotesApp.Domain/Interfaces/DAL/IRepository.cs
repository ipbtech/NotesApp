using Ardalis.Specification;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.Domain.Interfaces.DAL
{
    public interface IRepository<TEntity> : IRepositoryBase<TEntity> 
        where TEntity : class, IEntityId
    { }
}
