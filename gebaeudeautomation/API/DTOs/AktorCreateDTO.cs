using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AktorCreateDTO
    {
        [Required]
        public string AktorId { get; set; }

        // Name of Actor
        public string AktorName { get; set; }

        // Anything interesting regarding the sensor: Location, Limits etc.
        public string Description { get; set; }

        public string AktorType { get; set; }
    }
}