namespace API.Data.Models
{
    public class Aktor
    {
        // Id of Actor
        public string AktorId { get; set; }

        // Name of Actor
        public string AktorName { get; set; }

        // Anything interesting regarding the sensor: Location, Limits etc.
        public string Description { get; set; }

        // Last "message" from Actor to backend
        public double LastSeen { get; set; }

        public string AktorType { get; set; }
    }
}