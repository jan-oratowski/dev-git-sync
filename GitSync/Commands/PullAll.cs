using System;
using System.Collections.Generic;
using System.Text;

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
            foreach (var directory in System.IO.Directory.GetDirectories(pathToRepos))
            {
                var git = new GitCommands(directory);
                git.Pull();
            }
        }
    }
}
