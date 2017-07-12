using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WlToolsLib.Producer;

namespace WlToolsLib.LogHelper
{
    class OldLog
    {
    }
    #region --并发日志处理--
    //日志实体
    public class LogEntity { public long LogID { get; set; } public DateTime LogTime { get; set; } public string LogSource { get; set; } public string LogInfo { get; set; } }

    public class FileLog<T> : TaskQueue<T>, IQueue<T> where T : LogEntity
    {
        public static FileLog<T> _instance = null;
        public static object objLock = new object();
        public static long alert_interval = 9000000000;

        //public Action<T> ProcessItem = null; 
        public FileLog()
        {

        }
        public static FileLog<T> CreateInstance()
        {
            if (_instance == null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                    {
                        _instance = new FileLog<T>();
                    }
                }
            }
            return _instance;
        }
        /// <summary>
        /// 日志分类
        /// </summary>
        private static string LOGCATA = ConfigurationManager.AppSettings["LogCata"];

        /// <summary>
        /// 日志处理任务
        /// </summary>
        public override void ProcessTask()
        {
            if (ConsumeFlag == 0)
            {
                Interlocked.Exchange(ref ConsumeFlag, 1);
                string logFile = LOGCATA + "\\" + DateTime.Today.ToString("yyyy-MM-dd") + ".test.log";
                try
                {
                    using (StreamWriter myStream = File.AppendText(@logFile))
                    {
                        while (ConsumeFlag == 1)
                        {
                            var t = GetItem();
                            if (t == null || t.Equals(default(T)))
                            {
                                Interlocked.Exchange(ref ConsumeFlag, 0);
                                break;
                            }
                            else
                            {
                                myStream.WriteLine("\r\n====操作时间[{0}]====\r\n{1}\r\n错误信息：{2}\r\n=====\r\n", t.Time.ToString("yyyy-MM-dd HH:mm:ss.fff"), t.Info, t.Info);
                                myStream.Flush();
                            }
                        }
                        myStream.Close();
                    }
                }
                catch { }
            }
            //GC.Collect();
        }

        /// <summary>
        /// 日志处理项
        /// </summary>
        /// <param name="t"></param>
        public override void ProcessItem(T t)
        {
            string logFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Today.ToString("yyyy-MM-dd") + ".log";
            using (StreamWriter myStream = File.AppendText(@logFile))
            {
                myStream.WriteLine("{0}\r\n操作时间：{1}\r\n错误原因：{2}", t.LogSource, t.LogTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), t.LogInfo);
                myStream.WriteLine("\r\n==============================\r\n");
                myStream.Flush();
                myStream.Close();
            }
        }
    }
    public class FProducer<T> : BaseProducer<T>
    {
        public FProducer(TaskQueue<T> queue) : base(queue)
        {
        }
    }
    public class FConsumer<T> : BaseConsumer<T>
    {
        public FConsumer(TaskQueue<T> queue) : base(queue)
        {
        }
    }
    #endregion
}
