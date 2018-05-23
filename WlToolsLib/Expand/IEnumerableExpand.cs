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
            for (int i = 0; i < source.Count(); i++)
            {
                var souTemp = source.ElementAt(i);
                yield return new Tuple<int, T>(i, souTemp);
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
    }
}
