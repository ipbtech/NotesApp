using NotesApp.Domain.Entities.Base;

namespace NotesApp.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public IQueryable<T> GetAll();
        public Task<T?> GetAsync(int entityId);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
