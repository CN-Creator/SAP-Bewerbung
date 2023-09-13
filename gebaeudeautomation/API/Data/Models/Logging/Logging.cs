using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{
    public class Logging
    {
        [Key]
        public Guid LoggingId { get; set; }

        public long RequestTimestamp { get; set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

        public string RequestedRoute { get; set; }

        public string RequestedController { get; set; }

        public double Runtime { get; set; }

        public string clientIP { get; set; }

        public Guid? UserGuid { get; set; }
    }
}