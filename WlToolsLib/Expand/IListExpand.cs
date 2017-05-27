using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Expand
{
    public static class IListExpand
    {
        #region --移除指定或者保留指定--

        /// <summary>
        /// 移除或者保留元素 ：
        /// isRemove=true, predicate为true时会移除；
        /// isRemove=false, predicate为true时会保留（predicate为false时会移除）；
        /// 改变了原始数据，请小心使用，否则会发生“另程序员毛骨悚然的恐怖错误”！
        /// </summary>
        /// <typeparam name="T">具体类型</typeparam>
        /// <param name="self"> IList扩展 </param>
        /// <param name="isRemove">是否移除，条件成功移除，反之，条件成功保留</param>
        /// <param name="checker">判断谓词</param>
        public static void RemoveOrHold<TSource>(this IList<TSource> self, bool isRemove, Func<TSource, bool> predicate)
        {
            if (self != null && self.Count() > 0)
            {
                int count = self.Count();
                for (int i = count - 1; i >= 0; i--)
                {
                    var item = self.ElementAt(i);
                    if (predicate != null)
                    {
                        var predicateResult = predicate(item);
                        if (isRemove && predicateResult)
                        {
                            self.RemoveAt(i);
                        }
                        else if (!isRemove && !predicateResult)
                        {
                            self.RemoveAt(i);
                        }
                    }
                }
            }
        }

        #endregion --移除指定或者保留指定--

        #region --创建一个属于 IList 的 foreach循环--

        /// <summary>
        /// 创建一个属于 IList 的 foreach循环
        /// </summary>
        /// <typeparam name="TData">容器内实体</typeparam>
        /// <param name="self">队列容器</param>
        /// <param name="action">执行动作</param>
        public static void Foreach<TData>(this IList<TData> self, Action<TData> action)
        {
            foreach (var item in self)
            {
                action(item);
            }
        }

        #endregion --创建一个属于 IList 的 foreach循环--

        #region --给IList加入AddRange--
        /// <summary>
        /// 给IList加入AddRange
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public static IList<T> AddRange<T>(this IList<T> self, IList<T> itemList)
        {
            if (itemList == null || !itemList.Any() || self == null)
            {
                return null;
            }
            else
            {
                foreach (var item in itemList)
                {
                    if (!object.Equals(null, item))
                    {
                        self.Add(item);
                    }
                }
            }
            return self;
        }
        #endregion --给IList加入Add--
    }
}
