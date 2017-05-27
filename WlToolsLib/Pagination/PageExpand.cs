using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Pagination
{
    public static class PaginationExpand
    {
        /// <summary>
        /// 分页扩展
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="self"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PaginationModel<TData> PageForList<TData>(this List<TData> self, int pageIndex, int pageSize)
        {
            if (self == null || !self.Any()) return PaginationModel<TData>.CreatePageData(pageSize, pageIndex, 0);
            PaginationModel<TData> p = PaginationModel<TData>.CreatePageData(pageSize, pageIndex, self.Count);
            // 分页 Skip跳过前[p.PageStartCount]条，Take获取[Size]条数据
            var pageOrder = self.Skip(p.PageStartCount).Take(p.PageSize).ToList();
            p.AddItems(pageOrder);
            return p;
        }
    }
}
