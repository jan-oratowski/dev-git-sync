using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConfigTools;

namespace GitSync.Commands
{
    class New : ICommand
    {
        public string Argument => "new";

        public void DoWork()
        {
            var basePath = GetBasePath();

            var originUrl = ConsoleHelper.PromptInput("Enter git origin url");

            var repoName = originUrl
                .Split("/", StringSplitOptions.RemoveEmptyEntries)
                .Last()
                .Replace(".git", string.Empty);

            repoName = EditableString.Input("Enter repo name", repoName).Value;

            var git = new GitCommands(Path.Combine(basePath, repoName));
            git.Clone(originUrl);

            while (EditableString.Input("Add another remote? [Y/n]", "y").Value == "y")
            {
                Console.WriteLine("Enter remote url");
                var remoteUrl = Console.ReadLine();
                Console.WriteLine("Enter remote name");
                var remoteName = Console.ReadLine();
                git.AddRemote(remoteUrl, remoteName);
                git.Push(remoteName);
            }
        }

        private string GetBasePath()
        {
            if (Program.Config.Paths.Count == 0)
                return Program.Config.AddPathToConfig();

            var line = 0;

            Program.Config.Paths.ForEach(path =>
            {
                Console.WriteLine($"{line}. {path}");
                line++;
            });

            Console.WriteLine("Enter path number:");

            if (!int.TryParse(Console.ReadLine(), out line))
            {
                Console.WriteLine("Parsing error, try again.");
                return GetBasePath();
            }

            return Program.Config.Paths[line];
        }

    }
}
