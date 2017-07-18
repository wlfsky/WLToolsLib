using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WlToolsLib.Future.IOS
{
    /// <summary>
    /// （加入资源分类（3层类，抽象逻辑分类，不涉及具体））
    /// 资源抽象分类
    /// 永久资源，无过期时间，有或无数量；
    /// 永久但被占有资源；无过期时间（或很久才过期），但使用期间无法转移（租用物）；
    /// 有效期资源，有过期时间，有数量限制（）；
    /// 消耗资源，有过期时间（可忽视），有数量限制（有抽象分类）
    /// 
    /// </summary>
    public class Resource
    {
        public string PID { get; set; }
        public string PName { get; set; }

        public string Path { get; set; }

        public string RID { get; set; }
        public string ResourceName { get; set; }

        public DateTime OverDate { get; set; }

        public decimal 总数 { get; set; }
        public decimal 剩余 { get; set; }

        /// <summary>
        /// 占有状态
        /// </summary>
        public int 占有状态 { get; set; }
        public string StatusName { get; set; }
        public DateTime 预计释放时间 { get; set; }
        public DateTime 预订时间 { get; set; }

        public int 消耗状态 { get; set; }

        public string 存储位置 { get; set; }

    }

    /// <summary>
    /// 收入资源日志
    /// </summary>
    public class InLog {
        public string InLogID { get; set; }
        public string PID { get; set; }
        public string PName { get; set; }
        public string RID { get; set; }
        public string ResourceName{get;set;}
        public decimal InCount { get; set; }
        public int UnitID { get; set; }
        public string UnitName { get; set; }

        public string UserID { get; set; }
        public string UserName { get; set; }

    }


    public class OutLog
    {
        public string OutLogID { get; set; }
        public string PID { get; set; }
        public string PName { get; set; }
        public string RID { get; set; }
        public string ResourceName { get; set; }

        public DateTime 消耗时间 { get; set; }//出库时间
        public decimal 消耗数量 { get; set; }
        public string 备注 { get; set; }

        public DateTime 租用时间 { get; set; }
        public DateTime 租用结束时间 { get; set; }

        public string UserID { get; set; }
        public string UserName { get; set; }

    }

    /*
     * 入，入记录
     * 出，出记录
     * 帐务，入，出
     */
}
