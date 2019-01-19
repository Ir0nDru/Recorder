using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Hangfire;
using Recorder.Service;
using Recorder.Service.Services;
using System.Net.Http;
using Recorder.Service.Entities;
using RestSharp;

namespace Recorder.Executor
{
    class Program
    {

        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(@"Server=(localdb)\\mssqllocaldb;Database=Hangfire.Sample;Trusted_Connection=True;");
            using (new BackgroundJobServer())
            {
                var clientCameraService = new RestClient(new Uri("http://localhost:5000"));
                while (true)
                {
                    Thread.Sleep(15000);
                    var request = new RestRequest("cameras/actual", Method.GET);
                    Camera[] cameras = clientCameraService.ExecuteGetTaskAsync<Camera[]>(request)
                        .GetAwaiter()
                        .GetResult()
                        .Data;
                    foreach (Camera camera in cameras)
                    {
                        foreach (Record record in camera.Records)
                        {
                            var timeToRecord = Convert.ToInt32(DateTime.Now.Subtract(record.StartTime).TotalSeconds);
                            var recordingTimeSpan = Convert.ToInt32((record.EndTime - record.EndTime).TotalSeconds);

                            if (timeToRecord < 300.0)
                            {
                                BackgroundJob.Schedule(
                                    () => ExecutorHelper.MakeRecordTask(camera.IpAddress, recordingTimeSpan.ToString()),
                                    TimeSpan.FromSeconds(timeToRecord));
                            }
                        }
                    }
                }
            }
        }
    }
}
