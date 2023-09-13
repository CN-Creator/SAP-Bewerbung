using API.Data.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API.Data
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly DataContext _dbContext;
        public SensorDataRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddSensorDataToDatabase(SensorData sensorData)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync($@"
    INSERT INTO ""SensorsData"" (""SensorId"", ""Time"", ""Value"")
    VALUES (@SensorId, @Timestamp, @Value);
    ", new NpgsqlParameter("SensorId", sensorData.SensorId),
                new NpgsqlParameter("Timestamp", sensorData.Time),
                new NpgsqlParameter("Value", sensorData.Value)) > 0;

        }

        public async Task<List<SensorData>> GetDataDownload(string sensorId)
        {
            var data = _dbContext.SensorsData.FromSqlRaw($@"
    SELECT * FROM ""SensorsData"" WHERE ""SensorId""=@SensorId;",
    new NpgsqlParameter("SensorId", sensorId));

            // IQueryable data = _dbContext.Database.SqlQueryRaw($@"
            //     SELECT * FROM @Tablename;
            //     ", new NpgsqlParameter("Tablename", tableName));

            return await data.ToListAsync();
        }

        public async Task<List<SensorData>> GetLastDay(string sensorId)
        {
            // var data = _dbContext.SensorsData.FromSqlRaw($@"
            //     SELECT
            //         time_bucket('5 minute', ""Time"") AS bucket,
            //         ""SensorId"",
            //         avg(""Value"")
            //     FROM ""SensorsData"" sData
            //     WHERE ""Time"" > now() - INTERVAL '1 day'
            //     AND ""SensorId"" = @SensorId
            //     GROUP BY bucket, ""SensorId""
            //     ORDER BY bucket, ""SensorId"";
            //     ", new NpgsqlParameter("SensorId", sensorId));

            var data = _dbContext.SensorsData.FromSqlRaw($@"
    SELECT
        *
    FROM ""SensorsData"" sData
    WHERE ""Time"" > now() - INTERVAL '1 hour'
    AND ""SensorId"" = @SensorId;
    ", new NpgsqlParameter("SensorId", sensorId));

            return await data.ToListAsync();
        }
    }
}