using System.ComponentModel.DataAnnotations;
using API.Data.Models;

namespace API.DTOs
{
    public class AddSensorDataDto
    {
        [Required]
        public string Timestamp { set; get; }

        [Required]
        public List<SensorDataDto> SensorsData { set; get; }
    }

    public class SensorDataDto
    {
        [Required]
        public string SensorId { set; get; }

        [Required]
        public string Value { set; get; }
    }
}