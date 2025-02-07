using Ardalis.Specification;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    { }
}
