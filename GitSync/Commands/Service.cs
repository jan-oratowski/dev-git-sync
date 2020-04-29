using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GitSync.Commands
{
    public class Service : ICommand
    {
        public string Argument => "service";

        public void DoWork()
        {
            while (true)
            {
                Program.Config.Paths.ForEach(ListAndSync);
                Logger.TrackEvent($"Service sleeping for {Program.Config.SyncEveryHours} hours");
                Thread.Sleep(new TimeSpan(0, Program.Config.SyncEveryHours, 0, 0));
            }
        }

        private void ListAndSync(string pathToRepos)
        {
            foreach (var directory in System.IO.Directory.GetDirectories(pathToRepos))
            {
                var git = new GitCommands(directory);
                git.Pull();
                git.PushAll();
            }
        }
    }
}
