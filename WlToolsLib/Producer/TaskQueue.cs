using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WlToolsLib.DataShell;

namespace WlToolsLib.Producer
{
    public abstract class TaskQueue<T> : IQueue<T>
    {
        public bool IsDebug { get; set; }
        protected static ConcurrentQueue<T> xQueue = new ConcurrentQueue<T>();
        public static long ConsumeFlag = 0;//0为运行，1预备运行，2正在运行
        public Action<string> Log;
        public void AddItem(T t)
        {
            xQueue.Enqueue(t);
        }

        public DataShell<T> GetItem()
        {
            T t = default(T);
            if (xQueue.TryDequeue(out t))
            {
                return DataShell<T>.CreateSuccess(t);
            }
            return DataShell<T>.CreateFail<T>();
        }

        public DataShell<T> TryGetItem()
        {
            T t = default(T);
            if (xQueue.TryPeek(out t))
            {
                return DataShell<T>.CreateSuccess(t);
            }
            return DataShell<T>.CreateFail<T>();
        }
        public virtual void ProcessTask()
        {
            if (Interlocked.Read(ref ConsumeFlag) == 0)
            {
                Interlocked.Exchange(ref ConsumeFlag, 1);
                while (Interlocked.Read(ref ConsumeFlag) == 1)
                {
                    var t = GetItem();
                    if (t.Success == true)
                    {
                        Interlocked.Exchange(ref ConsumeFlag, 0);
                        break;
                    }
                    else
                    {
                        ProcessItem(t.Data);
                    }
                }
            }
            //GC.Collect();
        }
        public abstract void ProcessItem(T t);
    }
}
