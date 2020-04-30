using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync.Commands
{
    class CommitAll : ICommand
    {
        public string Argument => "commit-all";
        public void DoWork()
        {
            throw new NotImplementedException();
        }
    }
}
