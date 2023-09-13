using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_backend.DTOs;
using todo_backend.Interfaces;

namespace todo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // Dependency injection of the UnitOfWork interface
        private readonly IUnitOfWork _unitOfWork;

        // Constructor to initialize the UnitOfWork interface
        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches all tasks.
        /// </summary>
        /// <returns>A list of tasks or NotFound if the list is empty.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            // Fetch tasks from the repository
            List<Models.Task>? tasks = await _unitOfWork.TaskRepository.GetTasks();

            // Check if the tasks list is null or empty
            if (tasks == null)
            {
                return NotFound();
            }
            return tasks;
        }

        /// <summary>
        /// Fetches a specific task by ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <returns>The task with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(long id)
        {
            // Fetch task by ID from the repository
            Models.Task? task = await _unitOfWork.TaskRepository.GetTaskById(id);

            // Check if the task is null
            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="task">The updated task object.</param>
        /// <returns>NoContent if updated successfully, or appropriate error messages.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(long id, UpdateTaskDTO task)
        {
            // Validate the ID
            if (id != task.Id)
            {
                return BadRequest();
            }

            // Update the task in the repository
            if (!await _unitOfWork.TaskRepository.UpdateTask(task))
            {
                return Problem("Error updating Task");
            }

            // Try to complete the changes in the unit of work
            try
            {
                if (!await _unitOfWork.Complete())
                {
                    return Problem("Error updating Task");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.TaskRepository.TaskExists(task.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">The task object to create.</param>
        /// <returns>The created task object or appropriate error messages.</returns>
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(CreateTaskDTO task)
        {
            // Save task to the repository
            _unitOfWork.TaskRepository.SaveTaskToDbAsync(task);

            // Check for successful completion
            if (await _unitOfWork.Complete())
            {
                // Fetch the newly created task
                Models.Task? taskNew = await _unitOfWork.TaskRepository.GetTaskByName(task.Name);

                // Error handling for null task
                if (taskNew == null)
                {
                    return Problem("Error after creating Task.");
                }
                return CreatedAtAction(nameof(GetTask), new { id = taskNew.Id }, taskNew);
            }

            return Problem("Task not created.");
        }

        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>NoContent if deleted successfully, or appropriate error messages.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(long id)
        {
            // Delete the task from the repository
            if (!await _unitOfWork.TaskRepository.DeleteTaskAsync(id))
            {
                return Problem("Error deleting Task");
            }

            // Check for successful completion
            if (await _unitOfWork.Complete())
            {
                return NoContent();
            }

            return Problem("Error deleting Task");
        }
    }
}
