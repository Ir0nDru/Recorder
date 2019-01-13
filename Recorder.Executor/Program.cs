using System;
using System.Diagnostics;

namespace Recorder.Executor
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "ping",
                Arguments = $"localhost",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
                //FileName = "cmd.exe",
                //Arguments = "dir",
                //RedirectStandardOutput = true,
                //CreateNoWindow = true,
                //UseShellExecute = false
            };
            Process proc = new Process
            {
                StartInfo = info
            };
            proc.Start();
            string str = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            Console.WriteLine(str);
            Console.ReadLine();
        }        
    }
}
