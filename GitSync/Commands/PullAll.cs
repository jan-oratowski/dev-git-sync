using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GitSync.Commands
{
    class PullAll : ICommand
    {
        public string Argument => "pull-all";
        public void DoWork()
        {
            Program.Config.Paths.ForEach(Pull);
        }

        private void Pull(string pathToRepos)
        {
            var tasks = new List<Task>();

            foreach (var directory in System.IO.Directory.GetDirectories(pathToRepos))
            {
                var task = Task.Factory.StartNew(() => PullSingle(directory));
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
        }

        private void PullSingle(string directory)
        {
            var git = new GitCommands(directory);
            git.Pull();
        }
    }
}
