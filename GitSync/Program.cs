using System;
using System.Collections.Generic;
using System.Threading;

namespace GitSync
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var repos = new List<string> {"gitlab", "azure"};
            var worker = new GitWorker(repos);

            var path = string.Empty;

            if (args.Length > 0)
                path = args[0];

            if (args.Length > 1)
                Logger.InitAppInsights(args[1]);

            while (true)
            {
                worker.Run(path);
                Logger.TrackEvent("Service sleeping for 6 hours");
                Thread.Sleep(new TimeSpan(6,0,0));
            }
            
        }
    }
}
