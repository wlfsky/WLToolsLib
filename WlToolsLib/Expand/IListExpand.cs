﻿using System;
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
            if (self != null && self.Any())
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
            if (action.IsNull())
            {
                return;
            }
            if(self.HasItem())
            {
                foreach (var item in self)
                {
                    action(item);
                }
            }
        }

        #endregion --创建一个属于 IList 的 foreach循环--

        /// <summary>
        /// 检查list是否含有值，判null和any
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool HasItem<T>(this IList<T> self)
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
            if (self.IsNull() || itemList.NoItem())
            {
                return self;
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

        #region --返回带index索引的foreach--

        /// <summary>
        /// 返回带index索引的foreach,0->n
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="source">源容器实体</param>
        /// <returns></returns>
        public static IEnumerable<Tuple<int, T>> ForIndex<T>(IEnumerable<T> source)
        {
            for (int i = 0; i < source.Count(); i++)
            {
                var souTemp = source.ElementAt(i);
                yield return new Tuple<int, T>(i, souTemp);
            }
        }

        /// <summary>
        /// 返回反序的带index索引的foreach，n->0
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="source">源容器实体</param>
        /// <returns></returns>
        public static IEnumerable<Tuple<int, T>> ReverseForIndex<T>(IEnumerable<T> source)
        {
            if (source.HasItem())
            {
                var total = source.Count();
                for (int i = total - 1; i >= 0; i--)
                {
                    var souTemp = source.ElementAt(i);
                    yield return new Tuple<int, T>(i, souTemp);
                }
            }
        }

        #endregion --返回带index索引的foreach--

        #region --IList多级排序--
        /// <summary>
        /// list静态方法入口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        public static IList<T> SortX<T>(this IList<T> self, Func<T, T, int>[] l)
        {
            var ls = self.ToList();
            ls.Sort((x, y) => { return SortMulti(x, y, l, 0); });
            return ls;
        }

        /// <summary>
        /// 递归多层排序方法，类似sort（（x，y）=>{ return X;}）的数组版本；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="l"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int SortMulti<T>(T x, T y, Func<T, T, int>[] l, int i)
        {
            var maxLen = l.Length - 1;
            if (i > maxLen)
            {
                return 0;
            }
            else
            {
                var xr = l[i](x, y);
                return xr != 0 ? xr : SortMulti(x, y, l, i + 1);
            }
        }

        /*func泛型委托建立时要确定类型，才能被使用否则会在ide中报错*/
        /*
         * 
        public static Func<T, T, Func<T, T, int>[], int> SotTX<T> = (x, y, l, r) =>{ return 0};
        public static Func<T, int> FT<T> = (t) => { return 0; };

         这是个 运行样例 参数构成 关键在 排序具体计算的定义
         new Func<Tuple<int, int>, Tuple<int, int>, int>[] { 
             (x, y) => { return x.Item1.CompareTo(y.Item1); }, 
             (x, y) => { return x.Item2.CompareTo(y.Item2); }
         }
         运行样例整体
        xl.SortX<Tuple<int, int>>(new Func<Tuple<int, int>, Tuple<int, int>, int>[] { (x, y) => { return x.Item1.CompareTo(y.Item1); }, (x, y) => { return x.Item2.CompareTo(y.Item2); } });
        */

        // 这里跟一个 别人写的 递归lambda 斐波那契递归，以上方法参考这个写法。但是实际上没有用这种方法还是用的函数
        public static readonly Func<int, int> Fibonacci = n => n > 1 ? Fibonacci(n - 1) + Fibonacci(n - 2) : n;
        #endregion
        
        #region --IList分离新旧两组List数据，常用于处理关系数据更新时分离新旧数据--
        /// <summary>
        /// 分离新旧两组List数据。自定义谓词识别是否相同
        /// 1:new=insert,2:old=del,3:nochange=null
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="newList"></param>
        /// <param name="oldList"></param>
        /// <param name="equalPredicate"></param>
        /// <returns>new=insert,old=del,nochange=null</returns>
        public static Tuple<IList<TData>, IList<TData>, IList<TData>> SeparationNewOld<TData>(this IList<TData> newList, IList<TData> oldList, Func<TData, TData, bool> equalPredicate)
        {
            if ((newList == null && oldList == null) || (!newList.Any() && !oldList.Any()))
            {
                throw new Exception("No data source.无数据源");
            }

            if (newList == null || !newList.Any())
            {
                var noChangeListTemp = oldList;
                return new Tuple<IList<TData>, IList<TData>, IList<TData>>(new List<TData>(), oldList, noChangeListTemp);
            }
            if (oldList == null || !oldList.Any())
            {
                return new Tuple<IList<TData>, IList<TData>, IList<TData>>(newList, new List<TData>(), new List<TData>());
            }

            IList<TData> noChangeList = new List<TData>();
            var newLen = newList.Count;

            for (int i = newLen - 1; i >= 0; i--)
            {
                var newItem = newList[i];
                var oldLen = oldList.Count;
                for (int j = oldLen - 1; j >= 0; j--)
                {
                    var oldItem = oldList[j];
                    if (equalPredicate(newItem, oldItem))
                    {
                        noChangeList.Add(newItem);
                        newList.RemoveAt(i);
                        oldList.RemoveAt(j);
                        break;
                    }
                }
            }
            // new=insert,old=del,nochange=
            return new Tuple<IList<TData>, IList<TData>, IList<TData>>(newList, oldList, noChangeList);
        }
        #endregion
            
        #region --接口转换实例--
        /// <summary>
        /// 接口转换实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> TransInstance<T>(this IList<T> self)
        {
            if(self.IsNull())
            {
                return default(List<T>);
            }
            if (self is List<T>)
            {
                return self as List<T>;
            }
            else
            {
                var t = new List<T>();
                t.AddRange(self.ToList());
                return t;
            }
        }
        #endregion
    }
}
