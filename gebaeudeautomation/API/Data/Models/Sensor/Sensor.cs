namespace API.Data.Models
{
    public class Sensor
    {
        // Name of the sensor is its id
        public string SensorId { get; set; }

        // Anything interesting regarding the sensor: Location, Limits etc.
        public string Description { get; set; }

        // Last "message" from sensor to backend
        public double LastSeen { get; set; }

        public bool DisplayOnDashboard { get; set; } = true;

        public string DisplayTitle { get; set; }

        public string ShortDescription { get; set; }

        public string ThumbnailPath { get; set; }

        public string SensorModel { get; set; }
    }
}