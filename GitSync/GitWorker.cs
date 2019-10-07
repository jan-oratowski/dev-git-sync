using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GitSync
{
    public class GitWorker
    {
        private readonly List<string> _services;

        public GitWorker(List<string> services)
        {
            _services = services;
        }

        public void Run(string pathToRepos = null)
        {
            if (string.IsNullOrEmpty(pathToRepos))
                pathToRepos = AppContext.BaseDirectory;

            foreach (var directory in System.IO.Directory.GetDirectories(pathToRepos))
            {
                PullGitRepo(directory);
                _services.ForEach(s => PushGitRepo(s, directory));
            }
        }

        private void PullGitRepo(string repoPath)
        {
            Console.WriteLine($"Pulling {repoPath}");
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "git.exe";
                process.StartInfo.Arguments = "pull origin";
                process.StartInfo.WorkingDirectory = repoPath;
                process.Start();
                process.WaitForExit();
            }
        }

        private void PushGitRepo(string service, string repoPath)
        {
            Console.WriteLine($"Pushing {repoPath} to {service}");
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "git.exe";
                process.StartInfo.Arguments = $"push {service}";
                process.StartInfo.WorkingDirectory = repoPath;
                process.Start();
                process.WaitForExit();
            }
        }

    }
}
