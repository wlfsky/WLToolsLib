

namespace WlToolsLib.Expand
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public static class IDictionaryExpand
    {
        /// <summary>
        /// 检查字典中是否有值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool HasItem<TKey, TValue>(this IDictionary<TKey, TValue> self)
        {
            if (self.NotNull() && self.Any())
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
