using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;

namespace WlToolsLib.Producer
{
    public interface IQueue<T>
    {
        void AddItem(T t);
        DataShell<T> GetItem();
        DataShell<T> TryGetItem();
        void ProcessTask();
        void ProcessItem(T t);
    }
}
