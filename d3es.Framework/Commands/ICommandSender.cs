using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Commands
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : ICommand;
    }
}
