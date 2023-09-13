using todo_backend.Interfaces;
using todo_backend.Models;

namespace todo_backend.Repositories
{
    // Implements the IUnitOfWork interface to provide a layer of abstraction over the DataContext for transactional operations
    public class UnitOfWork : IUnitOfWork
    {
        // Instance of DataContext for interacting with the database
        private readonly DataContext _context;

        // Constructor to initialize the DataContext instance
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Provides access to TaskRepository by lazy loading it with the current DataContext.
        /// </summary>
        public override ITaskRepository TaskRepository => new TaskRepository(_context);

        /// <summary>
        /// Saves all changes made to the DataContext asynchronously.
        /// </summary>
        /// <returns>True if at least one change was saved, False otherwise.</returns>
        public override async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Checks if there are any changes that have not been saved to the DataContext.
        /// </summary>
        /// <returns>True if there are unsaved changes, False otherwise.</returns>
        public override bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
