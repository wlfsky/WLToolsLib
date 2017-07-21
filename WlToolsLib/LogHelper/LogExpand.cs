// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    LogExpand
// CreateTime:  2017/07/12 18:15:06
// Creator:     weilai
// FileRemark:  
// ------------------------------------


namespace WlToolsLib.LogHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WlToolsLib.Expand;
    using WlToolsLib.JsonHelper;


    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LogExpand
    {
        public static readonly string LogTemplate = "[BLOG]\r\n[BMARK]{0}[EMARK]\r\n[BTIME]{1}[ETIME]\r\n[BCONTENT]{2}[ECONTENT]\r\n[ELOG]";

        public static string FormatLog(string mark, string content)
        {
            return string.Format(LogTemplate, mark, DateTime.Now.FullStr(), content);
        }

        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="self"></param>
        public static void InfoLog(this string self, string content)
        {
            if (self.NotNullEmpty())
            {
                Task.Factory.StartNew(() =>
                {
                    string logStr = FormatLog(self, content);
                    Log.GetInstance().InfoLog(logStr);
                });
            }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="self"></param>
        public static void ErrLog(this string self, string content)
        {
            if (self.NotNullEmpty())
            {
                Task.Factory.StartNew(() =>
                {
                    string logStr = FormatLog(self, content);
                    Log.GetInstance().ErrLog(self);
                });
            }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="self"></param>
        public static void ErrLog(this string self, Exception ex)
        {
            if (self.NotNull())
            {
                Task.Factory.StartNew(() =>
                {
                    string errStr = ex.ToJson();
                    string logStr = FormatLog(self, errStr);
                    Log.GetInstance().ErrLog(logStr);
                });
            }
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="self"></param>
        public static void DebugLog(this string self, string content)
        {
            if (self.NotNullEmpty())
            {
                Task.Factory.StartNew(() =>
                {
                    string logStr = FormatLog(self, content);
                    Log.GetInstance().DebugLog(logStr);
                });
            }
        }
    }
}
