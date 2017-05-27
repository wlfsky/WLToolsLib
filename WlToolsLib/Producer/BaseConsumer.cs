using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Producer
{
    public class BaseConsumer<T>
    {
        public TaskQueue<T> Queue { get; set; }
        public BaseConsumer(TaskQueue<T> queue)
        {
            Queue = queue;
            //Queue.Log = (s) => { Console.WriteLine(s); };
        }
        public void Consume()
        {
            Queue.ProcessTask();
        }
    }
}
