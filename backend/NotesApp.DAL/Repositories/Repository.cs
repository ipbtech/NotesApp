using Ardalis.Specification.EntityFrameworkCore;
using NotesApp.Domain.Contracts.Repositories;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.DAL.Repositories
{
    public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> where TEntity : BaseEntity
    {
        public Repository(NotesAppDbContext dbContext) : base(dbContext)
        { }
    }
}
