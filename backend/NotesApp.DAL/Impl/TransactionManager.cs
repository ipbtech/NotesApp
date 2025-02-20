using System.Data;
using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Interfaces.DAL;

namespace NotesApp.DAL.Impl
{
    internal class TransactionManager(
        NotesAppDbContext dbContext) : ITransactionManager
    {
        public async Task ExecuteAsync(Func<Task> operation)
        {
            await using var transaction = await dbContext.Database
                .BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                await operation.Invoke();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
