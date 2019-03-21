

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Expand
{
    public static class DataCheckExpand
    {
        #region --错误检查扩展--
        /// <summary>
        /// 错误检查简化版，有错误返回错误信息，无错误返回空字符串
        /// </summary>
        /// <param name="self"></param>
        /// <remarks>
        /// how to use 如何使用
        /// var b = new Book();//注意，b来源外部，检查方法外部存在
        /// var c = new Dictionary<string, Func<bool>>()
        /// {
        ///     ["无数据|no data"] = () => b.IsNull(),
        ///     ["无id|no id"] = () => b.BookID <= 0,
        ///     ["无价格|no price"] = () => b.BookPrice <= 0m
        /// };
        /// var err = c.SimpleChecker();
        ///     if (err.NotNullEmpty())
        ///     {
        ///         // return error
        ///     }
        /// </remarks>
        /// <returns></returns>
        public static string SimpleChecker(this Dictionary<string, Func<bool>> self)
        {
            foreach (var key in self.Keys)
            {
                if (self[key].NotNull() && self[key]())
                {
                    return key;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 错误检查：返回值加强版。
        /// 有错误返回是否有错（注意是返回是否有错:true有错，false无错）
        /// </summary>
        /// <param name="self"></param>
        /// <returns>返回元祖，是否有错，无错则返回 false 和 空白字符串</returns>
        public static (bool haveerror, string info) Checker(this Dictionary<string, Func<bool>> self)
        {
            foreach (var eKey in self.Keys)
            {
                if (self[eKey].NotNull() && self[eKey]())
                {
                    return (true, eKey);
                }
            }
            return (false, string.Empty);
        }

        /// <summary>
        /// 错误检查常规版，自带检查规则
        /// 有错误返回是否有错（注意是返回是否有错：true有错，false无错）
        /// </summary>
        /// <param name="self"></param>
        /// <returns>返回元祖，是否有错，无错则返回 false 和 空白字符串</returns>
        public static (bool haveerror, string info) Checker<TData>(this Dictionary<string, Func<TData, bool>> self, TData data)
        {
            foreach (var eKey in self.Keys)
            {
                if (self[eKey].NotNull() && self[eKey](data))
                {
                    return (true, eKey);
                }
            }
            return (false, string.Empty);
        }

        /// <summary>
        /// 对列表的错误检查
        /// 任何一个数据出现错误就返回
        /// （注意是返回是否有错：true有错，false无错）
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="self"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static (bool haveerror, string info, TData data) CheckerList<TData>(this Dictionary<string, Func<TData, bool>> self, IEnumerable<TData> dataList)
        {
            if (dataList.HasItem())
            {
                foreach (var item in dataList)
                {
                    if (self.NotNull() && self.Keys.HasItem())
                    {
                        foreach (var eKey in self.Keys)
                        {
                            if (self[eKey].NotNull() && self[eKey](item))
                            {
                                return (true, eKey, item);
                            }
                        }
                    }
                }
            }
            return (false, string.Empty, default(TData));
        }
        #endregion
    }
    
    /// <summary>
    /// 错误检查结果(可能无用)
    /// </summary>
    public class CheckResult
    {
        public bool Success { get; set; }
        public string Info { get; set; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public void Deconstruct(out bool success, out string info) { success = Success; info = Info; }
    }

    /// <summary>
    /// 错误检查结果泛型版(可能无用)
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class CheckResult<TData> : CheckResult
    {
        public TData Data { get; set; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public void Deconstruct(out bool success, out string info, out TData data) { success = Success; info = Info; data = Data; }
    }
}
