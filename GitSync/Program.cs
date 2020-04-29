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
        private static List<ICommand> _commands;
        private static void Main(string[] args)
        {
            ConfigPath = GetArgument(args, "--config", "config.json");
            Config = Config.LoadConfig(ConfigPath);

            if (!string.IsNullOrEmpty(Config.AppInsights))
                Logger.InitAppInsights(Config.AppInsights);

            foreach (var s in args)
            {
                var command = _commands.FirstOrDefault(c => c.Argument == s);
                command?.DoWork();
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
