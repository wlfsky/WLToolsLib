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
        public static IEnumerable<Tuple<int, T>> ForeachForIndex<T>(IEnumerable<T> source)
        {
            for (int i = 0; i < source.Count(); i++)
            {
                var souTemp = source.ElementAt(i);
                yield return new Tuple<int, T>(i, souTemp);
            }
        }
    }
}
