using API.JobSystem.Jobs;
using Quartz;

namespace API.JobSystem;

public static class SchedulerHandler
{
    public static bool isStarted { private set; get; }

    public static async Task StartScheduler()
    {
        if (isStarted)
            return;
        Console.WriteLine("Scheduler is starting");
        await Start();
    }

    private static async Task Start()
    {
        await Scheduler.Start();
        isStarted = true;
        Console.WriteLine($"Scheduler is started: {isStarted}");
    }

    private static async Task AddJob(string jobId, IJobDetail detail, ITrigger trigger)
    {
        if (!isStarted)
        {
            Console.WriteLine("Scheduler is not running");
            return;
        }

        await Scheduler.AddJob(detail, trigger);
    }

    public static async Task CloseScheduler()
    {
        if (!isStarted)
        {
            Console.WriteLine("Scheduler cannot be close, since it hasn't been started before");
            return;
        }

        await Scheduler.Shutdown(true);
    }

    ///this is just for demonstration purposes, this should be handled by the backend dynamically
    public static async Task AddLEDJob(string aktorId)
    {
        if (!isStarted)
        {
            Console.WriteLine("Scheduler cannot be close, since it hasn't been started before");
            return;
        }

        IJobDetail jobDetail = JobBuilder.Create<CircadianLightJob>().WithIdentity(new JobKey(aktorId))
            .UsingJobData("aktorId", aktorId).Build();

        ITrigger trigger = TriggerBuilder.Create().WithIdentity($"{aktorId}-trigger")
            .WithSimpleSchedule(o => o.WithIntervalInMinutes(1).RepeatForever()).Build();

        await Scheduler.AddJob(jobDetail, trigger);
    }
}