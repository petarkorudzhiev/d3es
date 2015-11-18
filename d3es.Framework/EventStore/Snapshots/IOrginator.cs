using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.EventStore.Snapshots
{
    public interface IOrginator
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}
