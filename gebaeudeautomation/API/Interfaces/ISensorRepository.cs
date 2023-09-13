using System.Collections;
using API.Data.Models;

namespace API.Interfaces
{
    public interface ISensorRepository
    {
        void Update(Sensor sensor);

        Task<bool> DoesSensorWithNameExist(string sensorName);

        Task<IEnumerable<Sensor>> GetSensorsAsync(int currentPage, int maxPerPage);

        Task<IEnumerable<Sensor>> GetDisplayedSensorsAsync(int currentPage, int maxPerPage);

        Task<Sensor> GetSensorByIdAsync(string sensorId);

        void AddSensorToDatabase(Sensor sensor);
    }
}