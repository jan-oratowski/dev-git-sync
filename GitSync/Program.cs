using System;
using System.Collections.Generic;

namespace GitSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var repos = new List<string> {"gitlab"};
            var worker = new GitWorker(repos);

            worker.Run("C:\\test");
        }
    }
}
