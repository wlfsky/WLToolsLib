

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

        /// <summary>
        /// 字典 foreach
        /// 用 TKey, TValue 传入处理
        /// 当 TKey, TValue 是基础数据时是传入拷贝值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"> TKey, TValue </param>
        public static void Foreach<TKey, TValue>(this IDictionary<TKey, TValue> self, Action<TKey, TValue> action)
        {
            if (action.IsNull())
            {
                return;
            }
            if (self.HasItem())
            {
                foreach (var kv in self)
                {
                    action(kv.Key, kv.Value);
                }
            }
        }

        /// <summary>
        /// 字典 foreach
        /// 用 TKey, TValue 传入处理
        /// 会改变原字典的值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        public static void Foreach<TKey, TValue>(this IDictionary<TKey, TValue> self, Func<TKey, TValue, TValue> func)
        {
            if (func.IsNull())
            {
                return;
            }
            if (self.HasItem())
            {
                foreach (var kv in self)
                {
                    self[kv.Key] = func(kv.Key, kv.Value);
                }
            }
        }
    }
}
