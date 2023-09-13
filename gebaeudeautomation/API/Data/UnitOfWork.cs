using AutoMapper;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ILoggingRepository LoggingRepository => new LoggingRepository(_context, _mapper);

        public ISensorRepository SensorRepository => new SensorRepository(_context);
        public ISensorDataRepository SensorDataRepository => new SensorDataRepository(_context);
        public IAktorRepository AktorRepository => new AktorRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task<int> CreateSensorDatabase(string sensorId)
        {
            string tableName = $"{sensorId}_data";
            return await _context.Database.ExecuteSqlRawAsync($@"
        CREATE TABLE {tableName} (
            time TIMESTAMPTZ NOT NULL,
            value DOUBLE PRECISION NOT NULL
        );
        SELECT create_hypertable('{tableName}', 'time');
    ");
        }
    }
}