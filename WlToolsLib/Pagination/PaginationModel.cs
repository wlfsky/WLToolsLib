using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Pagination
{
    /// <summary>
    /// 分页实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginationModel<T>
    {
        public PaginationModel()
        {

        }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 实际每页记录数，最后一页记录数可能少于每页记录数
        /// </summary>
        public int ActualPageSize { get; set; }
        /// <summary>
        /// 页索引数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 当页起始记录数，为sql server分页sql语句准备的数据
        /// </summary>
        public int PageStartCount { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecordCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// SQL server下，分页第一次查询的数量
        /// </summary>
        public long TopCount { get; set; }
        /// <summary>
        /// 数据队列
        /// </summary>
        public List<T> PageContent { get; set; }
        /// <summary>
        /// 初始化分页
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引数</param>
        /// <param name="totalRecordCount">总记录数</param>
        protected PaginationModel(int pageSize, int pageIndex, int totalRecordCount)
        {
            this.PageSize = pageSize;
            this.TotalRecordCount = totalRecordCount;
            this.PageIndex = pageIndex;
            PageContent = new List<T>();
            Compute();
        }
        /// <summary>
        /// 用于快速的生成一个分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecordCount"></param>
        /// <returns></returns>
        public static PaginationModel<T> CreatePageData(int pageSize, int pageIndex, int totalRecordCount)
        {
            return new PaginationModel<T>(pageSize, pageIndex, totalRecordCount);
        }
        /// <summary>
        /// 计算分页数据
        /// </summary>
        protected virtual void Compute()
        {
            #region -- 计算分页数据 --
            PageSize = PageSize < 1 ? 20 : PageSize;//默认20
            int lastPage = Convert.ToInt32(TotalRecordCount % PageSize);//计算最后一页记录数
            TotalPageCount = Convert.ToInt32(TotalRecordCount / PageSize) + (lastPage > 0 ? 1 : 0);//计算总页数
            PageIndex = PageIndex > TotalPageCount ? TotalPageCount : PageIndex;//检查当前页数大
            PageIndex = PageIndex < 1 ? 1 : PageIndex;//检查当前页小
            TopCount = PageIndex * PageSize;//sqlite中用的 top 多少记录数，比sql server少pagesize个
            ActualPageSize = (PageIndex == TotalPageCount && lastPage != 0) ? lastPage : PageSize;//判断是否最后一页，并指定页记录数
            PageStartCount = (PageIndex - 1) * PageSize;//sql server用的
            #endregion -- 计算分页完成 --
        }
        /// <summary>
        /// 添加一个数据
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(T item)
        {
            if (this.PageContent != null)
            {
                this.PageContent.Add(item);
            }
        }
        /// <summary>
        /// 添加一组数据
        /// </summary>
        /// <param name="items"></param>
        public void AddItems(IEnumerable<T> items)
        {
            if (this.PageContent != null)
            {
                this.PageContent.AddRange(items);
            }
        }
    }
}
