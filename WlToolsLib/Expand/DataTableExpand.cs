// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    DataTableExpand
// CreateTime:  2017/07/12 16:46:05
// Creator:     weilai
// FileRemark:  
// ------------------------------------


namespace WlToolsLib.Expand
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public static class DataTableExpand
    {
        // <summary>
        /// 检查dataset是否有表数据
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool HasItem(this DataTable self)
        {
            if (self.NotNull() && self.Rows.NotNull() && self.Rows[0].NotNull())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
