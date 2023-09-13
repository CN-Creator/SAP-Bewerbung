using AutoMapper;
using API.Data.Models;
using API.Interfaces;

namespace API.Data
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public LoggingRepository(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void SaveLogToDBAsync(Logging loggingEntry)
        {
            _dbContext.Loggings.AddAsync(loggingEntry);
        }
    }
}