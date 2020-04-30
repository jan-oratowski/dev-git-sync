using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync.Commands
{
    class New : ICommand
    {
        public string Argument => "new";
        public void DoWork()
        {
            throw new NotImplementedException();
        }
    }
}
