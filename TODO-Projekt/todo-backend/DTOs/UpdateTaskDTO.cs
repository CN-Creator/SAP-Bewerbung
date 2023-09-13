namespace todo_backend.DTOs
{
    public class UpdateTaskDTO
    {
        public long Id { get; set; }

        public required string Name { get; set; }

        public bool IsComplete { get; set; } = false;
    }
}