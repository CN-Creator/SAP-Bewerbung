using Quartz;

namespace API.JobSystem.Jobs;

public class DummyJob: IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteAsync("Dummy Job ausgeführt!"+ context.FireTimeUtc +"\n");
    }
}