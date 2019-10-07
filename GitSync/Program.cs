using System;
using System.Collections.Generic;
using System.Threading;

namespace GitSync
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var repos = new List<string> {"gitlab"};
            var worker = new GitWorker(repos);

            var path = string.Empty;
            if (args.Length > 0)
                path = args[0];

            while (true)
            {
                worker.Run(path);
                Thread.Sleep(new TimeSpan(6,0,0));
            }
            
        }
    }
}
