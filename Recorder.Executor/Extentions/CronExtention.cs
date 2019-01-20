using Hangfire;

namespace Recorder.Executor.Extentions
{
    public static class CronExtention
    {
        public static string EverySecond() => new string("* 0 0 ? * * *");
        public static string SecondInterval(int interval) => new string($"0/{interval} 0 0 ? * * *");

    }
}
