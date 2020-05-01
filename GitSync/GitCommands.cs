using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GitSync
{
    class GitCommands
    {
        private readonly string _repoPath;

        public GitCommands(string repoPath)
        {
            _repoPath = repoPath;
        }

        public void Checkout(string origin)
        {

        }

        public void Commit()
        {
            Run("add .");
            Run($"commit -a -m \"Auto commit @ {DateTime.Now}\"");
        }

        public List<string> ListRemotes()
        {
            var remotes = Run("remote");
            return remotes.Split("\n\r", StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public void Pull(string remote = "origin")
        {
            Logger.TrackEvent($"Pulling {_repoPath} from {remote}");
            Run($"pull {remote}");
        }
        
        public void Push(string remote = "origin")
        {
            Logger.TrackEvent($"Pushing {_repoPath} to {remote}");
            Run($"push {remote}");
        }

        public void PushAll() => ListRemotes().ForEach(Push);

        private string Run(string arguments)
        {
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "git.exe";
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = _repoPath;

                process.Start();
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd();
            }
        }
    }
}
