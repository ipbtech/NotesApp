using Ardalis.Specification.EntityFrameworkCore;
using NotesApp.Domain.Entities.Base;
using NotesApp.Domain.Interfaces.Repositories;

namespace NotesApp.DAL.Repositories
{
    internal class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> where TEntity : BaseEntity
    {
        public Repository(NotesAppDbContext dbContext) : base(dbContext)
        { }
    }
}
