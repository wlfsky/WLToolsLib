
using System;
using System.Collections.Generic;

namespace WlToolsLib.Expand
{
    /// <summary>
    /// 枚举扩展方法类
    /// </summary>
    public static class EnumExpand
    {
        #region --枚举扩展--
        /// <summary>
        /// 枚举值翻译文字
        /// </summary>
        /// <param name="self"></param>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string EnumToStr(this Enum self, int enumValue)
        {
            var enumType = self.GetType();
            var result = enumType.GetEnumName(enumValue) ?? "NoValue";
            return result;//这里变量转一下只是为了方便debug
        }
        #endregion
    }
}

