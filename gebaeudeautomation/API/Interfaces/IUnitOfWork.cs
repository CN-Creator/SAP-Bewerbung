namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        ILoggingRepository LoggingRepository { get; }

        ISensorRepository SensorRepository { get; }

        ISensorDataRepository SensorDataRepository { get; }

        IAktorRepository AktorRepository { get; }

        Task<bool> Complete();

        bool HasChanges();
    }
}