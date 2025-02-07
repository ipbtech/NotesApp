using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Entities.Base;

namespace NotesApp.DAL.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly NotesAppDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(NotesAppDbContext dbContext) 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public async Task<TEntity?> GetAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
