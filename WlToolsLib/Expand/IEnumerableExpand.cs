using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Expand
{
    public static class IEnumerableExpand
    {
        /// <summary>
        /// 创建一个迭代器带有索引的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<int, T>> ForeachIndex<T>(this IEnumerable<T> source)
        {
            if (source.NotNull())
            {
                for (int i = 0; i < source.Count(); i++)
                {
                    var souTemp = source.ElementAt(i);
                    yield return new Tuple<int, T>(i, souTemp);
                }
            }
        }
        
        /// <summary>
        /// 反转的迭代器带有索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<int, T>> ReverseForeachIndex<T>(this IEnumerable<T> source)
        {
            if (source.NotNull())
            {
                var count = source.Count();
                for (int i = count - 1; i >= 0; i--)
                {
                    var souTemp = source.ElementAt(i);
                    yield return new Tuple<int, T>(i, souTemp);
                }
            }
        }

        /// <summary>
        /// 扩展IEnumerable<T> Foreach
        /// 调用此方法，如果没有 循环 好像会使循环不执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> self, Func<T, T> func)
        {
            // self 没有元素时，不会foreach
            foreach (var item in self)
            {
                yield return func(item);
            }
        }


        /// <summary>
        /// 扩展IEnumerable<T> Foreach
        /// 直接循环执行某个操作，无需包裹foreach
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this IEnumerable<T> self, Action<T> action)
        {
            // self 没有元素时，不会foreach
            foreach (var item in self)
            {
                action(item);
            }
        }


        /// <summary>
        /// 检查list是否含有值，判null和any
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool HasItem<T>(this IEnumerable<T> self)
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
        /// 枚举中没有项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NoItem<T>(this IEnumerable<T> self)
        {
            return !self.HasItem();
        }

        /// <summary>
        /// 用指定的字符串拼接由制定转换器转换出来的字符串组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="processor"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinBy<T>(this IEnumerable<T> self, Func<T, string> converter, string separator = ",")
        {
            if (converter.IsNull() || self.NoItem())
            {
                return string.Empty;
            }
            List<string> t = new List<string>();
            foreach (var i in self)
            {
                t.Add(converter(i));
            }
            string r = string.Join(separator, t);
            return r;
        }
        
        #region --数据对比--
        /// <summary>
        /// 自定义对比去重复
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items"></param>
        /// <param name="property"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TSource, bool> property, Func<TSource, TKey> expr)
        {
            EqualityComparer<TSource, TKey> comparer = new EqualityComparer<TSource, TKey>(property, expr);
            return items.Distinct(comparer);
        }

        #region --定义对比--
        /// <summary>
        /// 自定义对比
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        public class EqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
        {
            /// <summary>
            /// 初始化自定义对比
            /// </summary>
            /// <param name="comparer"></param>
            /// <param name="keyselecter"></param>
            public EqualityComparer(Func<TSource, TSource, bool> comparer, Func<TSource, TKey> keyselecter)
            {
                _comparer = comparer;
                _keyselecter = keyselecter;
            }
            /// <summary>
            /// 主键
            /// </summary>
            Func<TSource, TKey> _keyselecter = null;
            /// <summary>
            /// 对比器
            /// </summary>
            Func<TSource, TSource, bool> _comparer = null;
            /// <summary>
            /// 对比接口实现
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public bool Equals(TSource x, TSource y)
            {
                if (_comparer == null)
                {
                    _comparer = (m, n) => { return _keyselecter(m).GetHashCode().Equals(_keyselecter(n).GetHashCode()); };
                }
                return _comparer(x, y);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int GetHashCode(TSource obj)
            {
                return _keyselecter(obj).GetHashCode();
            }
        }
        #endregion
        #endregion
    }
}
