// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    DbConnException
// CreateTime:  2017/07/12 11:05:23
// Creator:     weilai
// FileRemark:  
// ------------------------------------


namespace WlToolsLib.DBHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 数据连接异常类
    /// </summary>
    public class DbConnException : Exception
    {
        public DbConnException(string msg) : base(msg)
        {

        }
    }
}
