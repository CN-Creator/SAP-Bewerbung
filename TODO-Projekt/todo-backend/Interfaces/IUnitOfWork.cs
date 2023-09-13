namespace todo_backend.Interfaces
{
    public abstract class IUnitOfWork
    {
        abstract public ITaskRepository TaskRepository { get; }

        abstract public Task<bool> Complete();

        abstract public bool HasChanges();
    }
}