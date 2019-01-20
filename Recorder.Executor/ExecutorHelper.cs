using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Newtonsoft.Json;
using Recorder.Service.Dto;
using Recorder.Service.Entities;
using RestSharp;
using RtspClientSharp;
using RtspClientSharp.Rtsp;

namespace Recorder.Executor
{
    public static class ExecutorHelper
    {
        private static RestClient clientCameraService = new RestClient(new Uri("http://localhost:5000/api"));
        public static void MakeRecordTask(string ipAdress, string timeSpan)
        {
            var serverUri = new Uri($"rtsp://{ipAdress}");
            var credentials = new NetworkCredential("admin", "Supervisor");
            var connectionParameters = new ConnectionParameters(serverUri, credentials);
            var cancellationTokenSource = new CancellationTokenSource();
            RecordAsync(connectionParameters, cancellationTokenSource.Token, ipAdress, timeSpan).Wait(CancellationToken.None);
        }

        private static async Task RecordAsync(ConnectionParameters connectionParameters, CancellationToken token, string ipAddress, string timeSpan)
        {
            try
            {
                TimeSpan delay = TimeSpan.FromSeconds(5);

                using (var rtspClient = new RtspClient(connectionParameters))
                {
                    Console.WriteLine("Connecting...");

                    try
                    {
                        await rtspClient.ConnectAsync(token);
                        Console.WriteLine("Connected");
                    }
                    catch (OperationCanceledException)
                    {
                    }
                    catch (RtspClientException e)
                    {
                        Console.WriteLine(e.ToString());
                        await Task.Delay(delay, token);
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            string args = $"-i rtsp://@{ipAddress} -t {timeSpan} -acodec copy -vcodec copy {DateTime.Now.ToFileTime().ToString()}.mp4";
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "ffmpeg.exe",
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                }
            };
            process.Start();
            process.WaitForExit();
        }

        public static void MakeRecordingTaskAsync()
        {

            var request = new RestRequest("cameras/actual/60", Method.GET);

            var response = clientCameraService.ExecuteGetTaskAsync<Camera[]>(request)
                .GetAwaiter()
                .GetResult();
            if (response.StatusCode != HttpStatusCode.OK)
                return;

            Camera[] cameras = JsonConvert.DeserializeObject<Camera[]>(response.Content);
            foreach (Camera cam in cameras)
            {
                foreach (Record record in cam.Records)
                {
                    var timeToRecord = Convert.ToInt32(DateTime.Now.Subtract(record.StartTime).TotalSeconds);
                    var recordingTimeSpan = Convert.ToInt32((record.EndTime - record.StartTime).TotalSeconds);

                    BackgroundJob.Schedule(
                        () => MakeRecordTask(cam.IpAddress, recordingTimeSpan.ToString()),
                        TimeSpan.FromSeconds(timeToRecord));

                    var successfullyUpdatedAwaiter = UpdateRecordStatusAsynk(record).ConfigureAwait(false).GetAwaiter();

                    successfullyUpdatedAwaiter.OnCompleted(() =>
                    {
                        if (successfullyUpdatedAwaiter.GetResult() == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"Failed to Update Status for recored with id {record.Id}!");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"Status for recored with id {record.Id} has updated successfully");
                        }

                        Console.ResetColor();
                    });
                }
            }
        }

        private static Task<bool> UpdateRecordStatusAsynk(Record record)
        {
            var updateRecordRequest = new RestRequest($"records/{record.Id}", Method.PUT);
            record.Status = RecordStatus.Recording;
            updateRecordRequest.AddXmlBody(record);
            var tcs = new TaskCompletionSource<bool>();
            clientCameraService.PutAsync(updateRecordRequest,
                (response, result) => tcs.SetResult(response.ResponseStatus == ResponseStatus.Completed)
            );
            return tcs.Task;
        }
    }
}
