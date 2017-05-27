using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Producer
{
    public class BaseProducer<T> : IProducer<T>
    {
        public TaskQueue<T> Queue { get; set; }
        public BaseProducer(TaskQueue<T> queue)
        {
            Queue = queue;
            //Queue.Log = (s) => { Console.WriteLine(s); };
        }
        public void Produc(T t)
        {
            Queue.AddItem(t);
        }
    }
}
