using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitSync.Commands
{
    class ListRemotes : ICommand
    {
        public string Argument => "list-remotes";

        public void DoWork()
        {
            foreach (var path in Program.Config.Paths)
            {
                foreach (var directory in Directory.GetDirectories(path))
                {
                    ListAndPrint(directory);
                }
            }
        }

        private void ListAndPrint(string path)
        {
            var git = new GitCommands(path);
            if (!git.IsGitRepository())
                return;

            var remotes = git.ListRemotes();
            Console.WriteLine();
            Console.WriteLine(path);
            remotes.ForEach(r => Console.WriteLine($" - {r}"));
        }
    }
}
