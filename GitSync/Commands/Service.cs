using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GitSync.Commands
{
    public class Service : ICommand
    {
        public string Argument => "service";

        public void DoWork()
        {
            while (true)
            {
                Program.Config.Paths.ForEach(p => ListAndSync(p));
                Logger.TrackEvent($"Service sleeping for {Program.Config.SyncEveryHours} hours");
                Thread.Sleep(new TimeSpan(0, Program.Config.SyncEveryHours, 0, 0));
            }
        }

        private void ListAndSync(string pathToRepos, bool doParallel = false)
        {
            var tasks = new List<Task>();

            foreach (var directory in System.IO.Directory.GetDirectories(pathToRepos))
            {
                var task = Task.Factory.StartNew(() => PullAndPushAll(directory));
                if (doParallel)
                {
                    tasks.Add(task);
                    continue;
                }

                task.Wait();
            }

            if (doParallel)
                Task.WaitAll(tasks.ToArray());
        }

        private void PullAndPushAll(string directory)
        {
            var git = new GitCommands(directory);
            if (!git.IsGitRepository())
                return;
            git.Pull();

            var remotes = git.ListRemotes();
            remotes.RemoveAll(r => r == "origin");

            git.PushAll(remotes);
        }
    }
}
