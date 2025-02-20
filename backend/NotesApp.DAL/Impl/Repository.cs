using Ardalis.Specification.EntityFrameworkCore;
using NotesApp.Domain.Interfaces.DAL;
using NotesApp.Domain.Interfaces.Entities;

namespace NotesApp.DAL.Impl
{
    internal class Repository<TEntity>(NotesAppDbContext dbContext) : 
        RepositoryBase<TEntity>(dbContext), IRepository<TEntity>
        where TEntity : class, IEntityId
    { }
}
