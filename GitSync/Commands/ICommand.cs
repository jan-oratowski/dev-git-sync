using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync.Commands
{
    internal interface ICommand
    {
        string Argument { get; }
        void DoWork();
    }
}
