using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;

namespace Recorder.Executor
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.Sleep(5000);
            GlobalConfiguration.Configuration.UseSqlServerStorage(@"Data Source=DESKTOP-BT6P7VD\SQLEXPRESS;Initial Catalog=HangfireDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            using (new BackgroundJobServer())
            {
                RecurringJob.AddOrUpdate(
                        () => ExecutorHelper.MakeRecordingTaskAsync(),
                        Cron.Minutely);
                Console.ReadKey();
            }
        }
    }
}
