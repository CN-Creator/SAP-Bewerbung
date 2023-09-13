using API.JobSystem.Jobs;
using Quartz;
using Quartz.Impl;

namespace API.JobSystem
{
    public class Scheduler
    {
        private static IScheduler _scheduler = null;
        
        //maybe be usefull or not
        public static readonly Dictionary<string, Type> allJobs = new Dictionary<string, Type>()
        {
            {"CircadianLightJob", typeof(CircadianLightJob)}
        };
        public static async Task InitScheduler()
        {
            if (_scheduler != null)
                return;
            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = await factory.GetScheduler();
        }
        
        public static async Task RemoveJob(string jobID)
        {
            if (_scheduler == null)
                await InitScheduler();

            await _scheduler.DeleteJob(new JobKey(jobID));
        }

        public static async Task AddJob(IJobDetail job, ITrigger trigger)
        {
            if (_scheduler == null)
                await InitScheduler();

            await _scheduler.ScheduleJob(job, trigger);
        }

        public static async Task UpdateJob(string jobID, ITrigger trigger)
        {
            if (_scheduler == null)
                await InitScheduler();

            var detail = await _scheduler.GetJobDetail(new JobKey(jobID));
            await _scheduler.ScheduleJob(detail, trigger);
        }

        public static async Task Start()
        {
            if (_scheduler == null)
                await InitScheduler();

            await _scheduler.Start();
        }

        public static async Task Shutdown(bool b)
        {
            if (_scheduler == null)
                await InitScheduler();

            await _scheduler.Shutdown();
        }
    }
}