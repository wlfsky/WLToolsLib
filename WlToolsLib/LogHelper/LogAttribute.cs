// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    LogAttribute
// CreateTime:  2017/07/12 18:16:55
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


    /// <summary>
    /// 日志特性
    /// </summary>
    public class LogAttribute : Attribute
    {
        public LogAttribute()
        {
            this.LogTime = DateTime.Now;
        }

        public DateTime LogTime { get; set; }
        public string LogFunc { get; set; }
        public string LogContent { get; set; }
        public string LogMark { get; set; }
    }
}
