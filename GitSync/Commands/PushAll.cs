using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync.Commands
{
    class PushAll : ICommand
    {
        public string Argument => "push-all";
        public void DoWork()
        {
            Program.Config.Paths.ForEach(Push);
        }

        private void Push(string pathToRepos)
        {
            foreach (var directory in System.IO.Directory.GetDirectories(pathToRepos))
            {
                var git = new GitCommands(directory);
                git.Push();
            }
        }
    }
}
