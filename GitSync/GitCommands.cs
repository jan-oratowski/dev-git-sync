using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public void AddRemote(string remoteUrl, string remoteName)
        {
            Run($"remote add {remoteName} {remoteUrl}");
        }

        public void Clone(string originUrl)
        {
            Run($"clone {originUrl} {_repoPath}", true);
        }

        public void Commit()
        {
            Run("add .");
            Run($"commit -a -m \"Auto commit @ {DateTime.Now}\"");
        }

        public bool IsGitRepository()
        {
            return Directory.GetDirectories(_repoPath).Any(d => d.Contains(".git"));
        }

        public List<string> ListRemotes()
        {
            var remotes = Run("remote");
            var list = remotes.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            return list;
        }

        public void Pull(string remote = "")
        {
            Logger.TrackEvent($"Pulling {_repoPath} from {remote}");
            Run($"pull {remote}");
        }

        public void Push(string remote = "origin")
        {
            Logger.TrackEvent($"Pushing {_repoPath} to {remote}");
            Run($"push {remote}");
        }

        public void PushAll(List<string> remotes) => remotes.ForEach(Push);

        private string Run(string arguments, bool ignoreDirectory = false)
        {
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "git.exe";
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = ignoreDirectory ? @"C:\" : _repoPath;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd();
            }
        }
    }
}
