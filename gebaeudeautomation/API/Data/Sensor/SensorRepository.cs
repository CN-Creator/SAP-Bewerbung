using API.Data.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SensorRepository : ISensorRepository
    {
        private readonly DataContext _dbContext;
        public SensorRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddSensorToDatabase(Sensor sensor)
        {
            _dbContext.Sensors.Add(sensor);
        }

        public async Task<bool> DoesSensorWithNameExist(string sensorName)
        {
            return await _dbContext.Sensors.AnyAsync(s => s.SensorId == sensorName);
        }

        public async Task<IEnumerable<Sensor>> GetSensorsAsync(int currentPage, int maxPerPage)
        {
            return await _dbContext.Sensors
                .OrderBy(s => s.SensorId)
                .Skip((currentPage - 1) * maxPerPage)
                .Take(maxPerPage)
                .ToListAsync();
        }

        public async Task<Sensor> GetSensorByIdAsync(string sensorId)
        {
            return await _dbContext.Sensors
            .SingleOrDefaultAsync(s => s.SensorId == sensorId);
        }

        public void Update(Sensor sensor)
        {
            _dbContext.Entry(sensor).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Sensor>> GetDisplayedSensorsAsync(int currentPage, int maxPerPage)
        {
            return await _dbContext.Sensors
                    .OrderBy(s => s.SensorId)
                    .Skip((currentPage - 1) * maxPerPage)
                    .Take(maxPerPage)
                    .Where(s => s.DisplayOnDashboard == true)
                    .ToListAsync();
        }
    }
}