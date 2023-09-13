namespace todo_backend.Models
{
    public class Task
    {
        public long Id { get; set; }

        public required string Name { get; set; }

        public long Created { get; set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

        public bool IsComplete { get; set; } = false;
    }
}