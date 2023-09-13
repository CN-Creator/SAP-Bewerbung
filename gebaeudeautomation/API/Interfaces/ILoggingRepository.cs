using API.Data.Models;

namespace API.Interfaces
{
    public interface ILoggingRepository
    {
        void SaveLogToDBAsync(Logging loggingEntry);
    }
}