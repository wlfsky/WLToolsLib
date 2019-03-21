using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.Expand
{
    /// <summary>
    /// 数字类型的扩展
    /// </summary>
    public static class NumberExpand
    {
        #region --decimal --
        /// <summary>
        /// 金额数据格式化
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToMoney(this decimal self)
        {
            return self.ToString("f2");
        }
        #endregion


        #region --抽象的对象对比--
        /// <summary>
        /// 自定义限制数据范围
        /// 自定义限制对比断言
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">可比大小的，且有范围的类型</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <param name="max_predicate">大比断言</param>
        /// <param name="min_predicate">小比断言</param>
        /// <returns></returns>
        public static T Limit<T>(this T self, T max, T min, Func<T, T, bool> max_predicate, Func<T, T, bool> min_predicate)
        {
            if (max_predicate(self, max))
            {
                self = max;
            }
            if (min_predicate(self, min))
            {
                self = min;
            }
            return self;
        }


        #endregion

        /// <summary>
        /// int类型限制数据范围
        /// 自动约束到范围内，超最大设置成最大，超最小设置成最小，其间不处理
        /// </summary>
        /// <param name="self">int类型</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public static int Limit(this int self, int max, int min)
        {
            return self.Limit<int>(max, min, (m, mx) => m > mx, (m, mn) => m < mn);
        }

        /// <summary>
        /// long类型限制数据范围
        /// 自动约束到范围内，超最大设置成最大，超最小设置成最小，其间不处理
        /// </summary>
        /// <param name="self">long类型</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public static long Limit(this long self, long max, long min)
        {
            return self.Limit<long>(max, min, (m, mx) => m > mx, (m, mn) => m < mn);
        }

        /// <summary>
        /// float类型限制数据范围
        /// 自动约束到范围内，超最大设置成最大，超最小设置成最小，其间不处理
        /// </summary>
        /// <param name="self">float类型</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public static float Limit(this float self, float max, float min)
        {
            return self.Limit<float>(max, min, (m, mx) => m > mx, (m, mn) => m < mn);
        }

        /// <summary>
        /// decimal 金额格式化
        /// </summary>
        /// <param name="self"></param>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public static string MoneyFmt(this decimal self, string formatStr = "f2")
        {
            return self.ToString(formatStr);
        }

        /// <summary>
        /// decimal? 金额格式化
        /// </summary>
        /// <param name="self"></param>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public static string MoneyFmt(this decimal? self, string formatStr = "f2")
        {
            return self.HasValue ? self.Value.ToString(formatStr) : (0.00m).MoneyFmt();
        }

        /// <summary>
        /// float 金额格式化
        /// </summary>
        /// <param name="self"></param>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public static string MoneyFmt(this float self, string formatStr = "f2")
        {
            return self.ToString(formatStr);
        }

        /// <summary>
        /// 字符串转换decimal，不能转就0.00返回
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static decimal StrToDecimal(this string self)
        {
            decimal r = 0.00m;
            if (self.NullEmpty())
            {
                return r;
            }
            decimal.TryParse(self, out r);
            return r;
        }


    }
}
