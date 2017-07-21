// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    Log
// CreateTime:  2017/07/12 18:13:53
// Creator:     weilai
// FileRemark:  
// ------------------------------------


namespace WlToolsLib.LogHelper
{
    using log4net;
    using log4net.Config;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WlToolsLib.Expand;


    /// <summary>
    /// 日志类
    /// </summary>
    public class Log
    {
        private static Log Logger { get; set; }

        public Log()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// 单例
        /// </summary>
        /// <returns></returns>
        public static Log GetInstance()
        {
            if (Logger.IsNull())
            {
                Logger = new Log();
            }
            return Logger;
        }

        /// <summary>
        /// 一般信息日志
        /// </summary>
        /// <param name="info"></param>
        public void InfoLog(string info)
        {
            ILog m_log = LogManager.GetLogger("InfoLogger");
            m_log.Info(info);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="err"></param>
        public void ErrLog(string err)
        {
            ILog m_log = LogManager.GetLogger("ErrorLogger");
            m_log.Error(err);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="debug"></param>
        public void DebugLog(string debug)
        {
            ILog m_log = LogManager.GetLogger("DebugLogger");
            m_log.Error(debug);
        }
    }
}
