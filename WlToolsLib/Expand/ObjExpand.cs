using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Expand
{
    public static class ObjExpand
    {
        #region --检查对象是否null--
        /// <summary>
        /// 检查对象是否null，是返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T self)
        {
            return ReferenceEquals(self, null);
        }

        /// <summary>
        /// 检查对象是否null 不是返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NotNull<T>(this T self)
        {
            return !self.IsNull();
        }
        #endregion --检查对象是否null--

        #region --拷贝对象--
        /// <summary>
        /// 将一个对象数据拷贝到另一个对象
        /// 表达式树的方式拷贝
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static TTarget CopyToObj<TSource, TTarget>(this TSource source, TTarget target)
        {
            return TransObj<TSource, TTarget>.Trans(source);
        }

        /// <summary>
        /// 拷贝对象数据到另一个对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        public static class TransObj<TSource, TTarget>
        {
            /// <summary>
            /// 定义一个委托操作
            /// </summary>
            private static readonly Func<TSource, TTarget> cache = GetFunc();

            /// <summary>
            /// 返回一个委托操作，这个操作是用表达式树构成的
            /// </summary>
            /// <returns></returns>
            private static Func<TSource, TTarget> GetFunc()
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "param");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();

                foreach (var item in typeof(TTarget).GetProperties())
                {
                    if (!item.CanWrite)
                    {
                        continue;
                    }
                    MemberExpression property = Expression.Property(parameterExpression, typeof(TSource).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TTarget)), memberBindingList.ToArray());
                Expression<Func<TSource, TTarget>> lambda = Expression.Lambda<Func<TSource, TTarget>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

                return lambda.Compile();
            }

            /// <summary>
            /// 对外公开的拷贝数据方法
            /// </summary>
            /// <param name="sou"></param>
            /// <returns></returns>
            public static TTarget Trans(TSource sou)
            {
                return cache(sou);
            }

        }
        #endregion

        #region --拷贝扩展--
        /// <summary>
        /// 用自定义的转换器转换拷贝
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <param name="exchanger">转换器</param>
        /// <returns></returns>
        public static TT TransCopy<TS, TT>(this TS self, Func<TS, TT> exchanger)
        {
            return exchanger(self);
        }

        /// <summary>
        /// 用单个转换直接转换列表数据
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self">转换器</param>
        /// <param name="sou">转换源</param>
        /// <returns></returns>
        public static IList<TT> TransList<TS, TT>(this Func<TS, TT> self, IList<TS> sou)
        {
            var rl = new List<TT>();
            foreach (var si in sou)
            {
                rl.Add(self(si));
            }
            return rl;
        }

        /// <summary>
        /// 以原队列为基础直接扩展队列转换
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self">源数据，无对象时返回null，无数据返回空队列</param>
        /// <param name="exchanger"></param>
        /// <returns></returns>
        public static IList<TT> TransList<TS, TT>(this IList<TS> self, Func<TS, TT> exchanger)
        {
            if(self.IsNull())
            {
                return default(IList<TT>);
            }
            var rl = new List<TT>();
            if(self.HasItem())
            {
                foreach (var si in self)
                {
                    rl.Add(exchanger(si));
                }
            }
            return rl;
        }

        /// <summary>
        /// 将单个转换委托，转换成列表转换委托
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Func<IList<TS>, IList<TT>> TransIListFunc<TS, TT>(this Func<TS, TT> self)
        {
            Func<IList<TS>, IList<TT>> tl = sl =>
            {
                var rl = new List<TT>();
                foreach (var si in sl)
                {
                    rl.Add(self(si));
                }
                return rl;
            };
            return tl;
        }

        /// <summary>
        /// 将单个转换委托，转换成列表转换委托
        /// 附带循环器
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <param name="looprefunc">循环中再处理</param>
        /// <returns></returns>
        public static Func<List<TS>, List<TT>> TransListFunc<TS, TT>(this Func<TS, TT> self, Func<TT, TT> looprefunc = null)
        {
            // 本地函数写法
            List<TT> tl(List<TS> sl)
            {
                var rl = new List<TT>();
                foreach (var si in sl)
                {
                    var t1 = self(si);
                    if (looprefunc.NotNull()) { t1 = looprefunc(t1); }
                    rl.Add(t1);
                }
                return rl;
            }
            return tl;
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
            if(self is List<T>)
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

        #region --过滤字符串两端空格--
        /// <summary>
        /// 去两端空格，对象为单位，不递归（如果出现循环引用递归会出问题）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static void ObjStrTrim<T>(this T self, bool filterSwitch = true, List<string> filterList = null) where T : class
        {
            if (self.NotNull())
            {
                var strType = typeof(string);
                var objType = self.GetType();
                foreach (var properItem in objType.GetProperties())
                {
                    if (properItem.PropertyType == strType)
                    {
                        var v = Convert.ToString(properItem.GetValue(self));
                        if (v.NotNullEmpty())
                        {
                            v = v.Trim();
                        }
                        properItem.SetValue(self, v);
                    }
                }
            }
        }
        #endregion
    }
}
