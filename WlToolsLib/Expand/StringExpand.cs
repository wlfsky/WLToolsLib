using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Expand
{
    public static class StringExpand
    {
        #region --简化验证--

        /// <summary>
        /// string.IsNullOrWiteSpace的简化版
        /// 支持检测null的字符类型（好tm强大）
        /// </summary>
        /// <param name="self">扩展字符串</param>
        /// <returns></returns>
        public static bool NullEmpty(this string self) => string.IsNullOrWhiteSpace(self);

        /// <summary>
        /// string.IsNullOrWiteSpace的简化版
        /// 支持检测null的字符类型（好tm强大）
        /// </summary>
        /// <param name="self">扩展字符串</param>
        /// <returns></returns>
        public static bool NotNullEmpty(this string self) => !self.NullEmpty();

        /// <summary>
        /// null或者空字符串返回true，空格和有值false
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NullStr(this string self) => string.IsNullOrEmpty(self);

        /// <summary>
        /// 不为null或者空字符串返回true，null或者空字符串返回false
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NotNullStr(this string self) => !self.NullStr();

        #endregion --简化验证--

        /// <summary>
        /// 返回两个字符串 之间的字符串，去两端空格
        /// </summary>
        /// <param name="self"></param>
        /// <param name="leftStr"></param>
        /// <param name="rightStr"></param>
        /// <returns></returns>
        public static string SinceStr(this string self, string leftStr, string rightStr)
        {
            if (self.NullEmpty()) return string.Empty;
            if (leftStr.NullStr() && rightStr.NullStr()) return self;
            var s = self.IndexOf(leftStr);
            var e = self.IndexOf(rightStr);
            if (e <= s) return string.Empty;
            if (s < 0 && e < 0) return self;
            if (s < 0) s = 0;
            if (e < 0) e = self.Length;
            var leftLen = leftStr.Length;
            var l = e - s - leftLen;
            if (l <= 0) return string.Empty;
            var sinceStr = self.Substring(s + leftLen, l);
            if (sinceStr.NullEmpty()) return string.Empty;
            return sinceStr.Trim();
        }

        /// <summary>
        /// 仿python切片,string版本
        /// </summary>
        /// <param name="self"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string Slice(this string self, int? s = null, int? e = null, int? step = null)
        {
            /*两边都没有返回原值*/
            if (self.IsNull()) return self;
            if (!s.HasValue && !e.HasValue) return self;
            if (!s.HasValue && e.HasValue) { return self.Substring(0, e.Value); }
            if (s.HasValue && !e.HasValue) { return self.Substring(s.Value, self.Length - s.Value); }
            return self;
        }

        /// <summary>
        /// 去两端空格，如果无值或者null原样返回，不会报异常
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string XTrim(this string self)
        {
            if (self.IsNotNull())
            {
                self = self.Trim();
                return self;
            }
            return self;
        }
    }
}
