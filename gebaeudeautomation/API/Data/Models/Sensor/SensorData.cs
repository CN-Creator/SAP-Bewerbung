
using Microsoft.EntityFrameworkCore;

namespace API.Data.Models
{
    [Keyless]
    public class SensorData
    {
        public string SensorId { set; get; }

        public Double Value { set; get; }

        public DateTimeOffset Time { set; get; }
    }
}