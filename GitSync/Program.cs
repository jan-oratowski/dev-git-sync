using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GitSync.Commands;
using GitSync.Models;

namespace GitSync
{
    internal class Program
    {
        public static string ConfigPath;
        public static Config Config;
        private static readonly List<ICommand> Commands = new List<ICommand>();

        private static void Main(string[] args)
        {
            ConfigPath = GetArgument(args, "--config", "config.json");
            Config = Config.Load(ConfigPath);

            if (!string.IsNullOrEmpty(Config.AppInsights))
                Logger.InitAppInsights(Config.AppInsights, "GitSync");

            RegisterCommands();

            foreach (var s in args)
            {
                var command = Commands.FirstOrDefault(c => c.Argument == s);
                command?.DoWork();
            }
        }

        private static string GetArgument(string[] args, string argument, string defaultValue = null)
        {
            return args.Any(a => a.StartsWith($"{argument}="))
                ? args.First(a => a.StartsWith($"{argument}=")).Replace($"{argument}=", string.Empty)
                : defaultValue;
        }

        private static void RegisterCommands()
        {
            Commands.Add(new CommitAll());
            Commands.Add(new PullAll());
            Commands.Add(new PushAll());
            Commands.Add(new Service());
        }
    }
}
