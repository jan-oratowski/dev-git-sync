using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
                .Replace(".git",string.Empty);

            repoName = ConsoleHelper.PromptInput("Enter repo name", repoName);

            var git = new GitCommands(Path.Combine(basePath, repoName));
            
                
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
