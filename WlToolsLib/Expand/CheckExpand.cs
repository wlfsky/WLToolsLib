

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WlToolsLib.Expand
{
    public static class ExpandMothed
    {
        #region --错误检查扩展--
        /// <summary>
        /// 错误检查简化版，有错误返回错误信息，无错误返回空字符串
        /// </summary>
        /// <param name="self"></param>
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
        /// 有错误返回是否有错（注意是返回是否有错 true有错，false无错）
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static (bool success, string info) Checker(this Dictionary<string, Func<bool>> self)
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
        /// 错误检查常规版，有错误返回是否有错（注意是返回是否有错 true有错，false无错）
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static (bool success, string info) Checker<TData>(this Dictionary<string, Func<TData, bool>> self, TData data)
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
        /// 任何一个数据出现错误
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="self"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static (bool success, string info, TData data) CheckerList<TData>(this Dictionary<string, Func<TData, bool>> self, IEnumerable<TData> dataList)
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
}
