using todo_backend.DTOs;

namespace todo_backend.Interfaces
{
    public abstract class ITaskRepository
    {
        abstract public void SaveTaskToDbAsync(CreateTaskDTO taskToSave);

        abstract public Task<List<Models.Task>?> GetTasks();

        abstract public Task<Models.Task?> GetTaskById(long id);

        abstract public Task<Models.Task?> GetTaskByName(string name);

        abstract public Task<bool> DeleteTaskAsync(long id);

        abstract public Task<bool> UpdateTask(UpdateTaskDTO updateTask);

        abstract public bool TaskExists(long id);

    }
}