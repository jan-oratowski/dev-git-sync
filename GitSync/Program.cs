using System;
using System.Linq;
using System.Threading;
using GitSync.Models;

namespace GitSync
{
    internal class Program
    {
        public static string ConfigPath;
        public static Config Config;
        private static void Main(string[] args)
        {
            ConfigPath = GetArgument(args, "--config", "config.json");
            Config = Config.LoadConfig(ConfigPath);

            if (!string.IsNullOrEmpty(Config.AppInsights))
                Logger.InitAppInsights(Config.AppInsights);

            var worker = new GitWorker(repos);

            while (true)
            {
                worker.Run(path);
                Logger.TrackEvent("Service sleeping for 6 hours");
                Thread.Sleep(new TimeSpan(6, 0, 0));
            }

        }

        private static string GetArgument(string[] args, string argument, string defaultValue = null)
        {
            return args.Any(a => a.StartsWith($"{argument}="))
                ? args.First(a => a.StartsWith($"{argument}=")).Replace($"{argument}=", string.Empty)
                : defaultValue;
        }

    }
}
