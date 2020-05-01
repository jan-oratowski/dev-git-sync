using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    public static class ConsoleHelper
    {
        public static string PromptInput(string prompt, string defaultValue = null)
        {
            Console.WriteLine(prompt);
            if (!string.IsNullOrEmpty(defaultValue))
                Console.WriteLine($"[{defaultValue}]");
            
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
                line = defaultValue;
            
            return line;
        }
    }
}
