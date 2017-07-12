using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
