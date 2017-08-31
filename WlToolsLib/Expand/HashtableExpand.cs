using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Expand
{
    public static class HashtableExpand
    {
        /// <summary>
        /// Hashtable 扩展是否有值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool HasItem(this Hashtable self)
        {
            if (self.IsNull())
            {
                return false;
            }
            var hasItem = false;
            foreach (var item in self)
            {
                hasItem = true;
                break;
            }
            return hasItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue>> Foreach<TKey, TValue>(this Hashtable self, Func<TKey, TValue, KeyValuePair<TKey, TValue>> func)
        {
            if (self.HasItem())
            {
                foreach (var item in self.Keys)
                {
                    if (func.NotNull())
                    {
                        yield return func((TKey)item, (TValue)self[item]);
                    }
                    else
                    {
                        yield return new KeyValuePair<TKey, TValue>((TKey)item, (TValue)self[item]);
                    }
                }
            }
        }
    }
}
