namespace NotesApp.Domain.Interfaces.DAL
{
    public interface ITransactionManager
    {
        /// <summary>
        /// ReadUncommited
        /// </summary>
        /// <returns></returns>
        public Task ExecuteAsync(Func<Task> operation);
    }
}
