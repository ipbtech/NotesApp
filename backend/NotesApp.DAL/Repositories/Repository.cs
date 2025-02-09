using Ardalis.Specification.EntityFrameworkCore;
using NotesApp.Domain.Interfaces.Entities;
using NotesApp.Domain.Interfaces.Repositories;

namespace NotesApp.DAL.Repositories
{
    internal class Repository<TEntity>(NotesAppDbContext dbContext) : 
        RepositoryBase<TEntity>(dbContext), IRepository<TEntity>
        where TEntity : class, IEntityId, IAuditable
    { }
}
