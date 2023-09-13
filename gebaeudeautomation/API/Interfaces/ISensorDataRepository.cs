using API.Data.Models;

namespace API.Interfaces
{
    public interface ISensorDataRepository
    {
        Task<bool> AddSensorDataToDatabase(SensorData sensorData);

        Task<List<SensorData>> GetDataDownload(string sensorId);

        Task<List<SensorData>> GetLastDay(string sensorId);
    }
}