using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class SensorCreateDTO
    {
        [Required]
        public string SensorId { get; set; }

        public string Description { get; set; }

        public bool DisplayOnDashboard { get; set; } = true;

        public string DisplayTitle { get; set; }

        public string ShortDescription { get; set; }

        public string ThumbnailPath { get; set; }

        public string SensorModel { get; set; }
    }
}