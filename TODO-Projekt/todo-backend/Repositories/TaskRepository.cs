using Microsoft.EntityFrameworkCore;
using todo_backend.DTOs;
using todo_backend.Interfaces;
using todo_backend.Models;

namespace todo_backend.Repositories
{
    // Implementation of the ITaskRepository interface
    public class TaskRepository : ITaskRepository
    {
        // DataContext instance for interacting with the database
        private readonly DataContext _context;

        // Constructor to initialize the DataContext instance
        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>True if successful, False otherwise.</returns>
        public override async Task<bool> DeleteTaskAsync(long id)
        {
            // Check if the Tasks DbSet is null
            if (_context.Tasks == null)
            {
                return false;
            }

            // Find the task with the given ID
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }

            // Remove the task from the DbSet
            _context.Tasks.Remove(task);

            return true;
        }

        /// <summary>
        /// Fetches a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <returns>The task with the specified ID or null.</returns>
        public override async Task<Models.Task?> GetTaskById(long id)
        {
            if (_context.Tasks == null)
            {
                return null;
            }
            return await _context.Tasks.FindAsync(id);
        }

        /// <summary>
        /// Fetches a task by its name.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <returns>The task with the specified name or null.</returns>
        public override async Task<Models.Task?> GetTaskByName(string name)
        {
            if (_context.Tasks == null)
            {
                return null;
            }
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Name == name);
        }

        /// <summary>
        /// Fetches all tasks.
        /// </summary>
        /// <returns>A list of tasks or null.</returns>
        public override async Task<List<Models.Task>?> GetTasks()
        {
            if (_context.Tasks == null)
            {
                return null;
            }
            return await _context.Tasks.ToListAsync();
        }

        /// <summary>
        /// Saves a new task to the database.
        /// </summary>
        /// <param name="taskToSave">The task object to save.</param>
        public override void SaveTaskToDbAsync(CreateTaskDTO taskToSave)
        {
            if (_context.Tasks == null)
            {
                return;
            }

            // Create and add a new task to the DbSet
            _context.Tasks.Add(new Models.Task { Name = taskToSave.Name });
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="updateTask">The updated task object.</param>
        /// <returns>True if successful, False otherwise.</returns>
        public override async Task<bool> UpdateTask(UpdateTaskDTO updateTask)
        {
            if (_context.Tasks == null)
            {
                return false;
            }

            // Fetch the existing task by ID
            Models.Task? task = await GetTaskById(updateTask.Id);
            if (task == null)
            {
                return false;
            }

            // Update the task properties
            task.Name = updateTask.Name;
            task.IsComplete = updateTask.IsComplete;

            // Mark the task as modified
            _context.Entry(task).State = EntityState.Modified;
            
            return true;
        }

        /// <summary>
        /// Checks if a task with a given ID exists.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <returns>True if exists, False otherwise.</returns>
        public override bool TaskExists(long id)
        {
            if (_context.Tasks == null)
            {
                return false;
            }

            // Check if any task exists with the given ID
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}