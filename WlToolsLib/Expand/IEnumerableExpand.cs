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
        public static IEnumerable<Tuple<int, T>> ForIndex<T>(this IEnumerable<T> source)
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
        /// 反转的迭代器带有索引,n->0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<int, T>> ReverseForIndex<T>(this IEnumerable<T> source)
        {
            if (source.HasItem())
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

        #region --是否重复--
        /// <summary>
        /// 元素是否重复
        /// 自定义对比方式，不提供默认是指对比（Equals）；
        /// 自身绕开用下标相等方法绕开；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool HaveRepeated<T>(this IEnumerable<T> source, Func<T, T, bool> comparer = null)
        {
            if (source != null && source.Any())
            {
                Dictionary<T, T> dic = new Dictionary<T, T>();
                int out_idx = 0;// 通过下标对比，绕开自身的对比
                foreach (var item in source)
                {
                    int in_idx = 0;
                    foreach (var iteminside in source)
                    {
                        if (out_idx == in_idx)
                        {
                            in_idx++;
                            continue;
                        }
                        if (comparer == null) { comparer = (x, y) => { return x.Equals(y); }; }
                        if (comparer(item, iteminside))
                        {
                            return true;
                        }
                        in_idx++;
                    }
                    out_idx++;
                }
                return false;
            }
            return false;
        }
        #endregion


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

        #region --去除重复的元素。生成新非重复元素组返回.无元素时原样返回 -- RemoveSame<T>--
        /// <summary>
        /// 去除重复的元素。生成新非重复元素组返回.无元素时原样返回
        /// 借助了Dictionary，的去重能力
        /// </summary>
        /// <typeparam name="T">主体类型</typeparam>
        /// <param name="self"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IEnumerable<T> RemoveSame<T>(this IEnumerable<T> self)
        {
            if (self.NoItem())
            {
                return self;
            }
            Dictionary<T, int> temp_dic = new Dictionary<T, int>();
            foreach (var item in self)
            {
                if (!temp_dic.ContainsKey(item))
                {
                    temp_dic.Add(item, 1);
                }
            }
            var result = temp_dic.Keys;
            return result;
        }
        /// <summary>
        /// 返回一组不不重复的 IEnumerable<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate">自定义重复断言, 不带此参数用dic的key直接对比元素</param>
        /// <returns></returns>
        public static IEnumerable<T> NoRepeat<T>(this IEnumerable<T> self, Func<T, T, bool> predicate = null)
        {
            if (self.HasItem() && predicate.IsNull())
            {
                // 没有对比断言 用字典key实现对比
                Dictionary<T, int> temp_dic = new Dictionary<T, int>();
                foreach (var item in self)
                {
                    if (!temp_dic.ContainsKey(item))
                    {
                        temp_dic.Add(item, 1);
                        yield return item;
                    }
                }
            }
            else if (self.HasItem() && predicate.NotNull())
            {
                foreach (var (k, v) in self.ForIndex())
                {
                    foreach (var (rk, rv) in self.ReverseForIndex())
                    {
                        if (k != rk && !predicate(v, rv))//排除自身，不相等返回
                        {
                            yield return v;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
