using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RtspClientSharp;
using RtspClientSharp.Rtsp;

namespace Recorder.Executor
{
    public static class ExecutorHelper
    {
        public static void MakeRecordTask(string ipAdress, string timeSpan)
        {
            var serverUri = new Uri($"rtsp://{ipAdress}");
            var credentials = new NetworkCredential("admin", "Supervisor");
            var connectionParameters = new ConnectionParameters(serverUri, credentials);
            var cancellationTokenSource = new CancellationTokenSource();
            RecordAsync(connectionParameters, cancellationTokenSource.Token, ipAdress, timeSpan).Wait(CancellationToken.None);
        }

        private static async Task RecordAsync(ConnectionParameters connectionParameters, CancellationToken token, string ipAdress, string timeSpan)
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
            string args = $"-i rtsp://@{ipAdress} -t {timeSpan} -acodec copy -vcodec copy {DateTime.Now.ToFileTime().ToString()}.mp4";
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
    }
}
