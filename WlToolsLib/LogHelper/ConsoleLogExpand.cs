using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.Expand;
using WlToolsLib.JsonHelper;
using static System.Console;

namespace WlToolsLib.LogHelper
{
    public static class ConsoleLogExpand
    {
        #region --屏幕日志记录--
        /// <summary>
        /// 屏幕日志记录 带有对象转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        public static void Log<T>(this T o, string title = "")
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine($"===={title}=={DateTime.Now.FullStr()}====");
            ForegroundColor = ConsoleColor.Green;
            WriteLine(o.ToJson());
        }

        /// <summary>
        /// 屏幕日志记录
        /// </summary>
        /// <param name="i"></param>
        public static void Log(this string i, string title = "")
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine($"===={title}=={DateTime.Now.FullStr()}====");
            ForegroundColor = ConsoleColor.Green;
            WriteLine(i);
        }
        #endregion --屏幕日志记录--

        #region --日志格式化--
        /// <summary>
        /// 将当前字符串格式化成日志文本
        /// </summary>
        /// <param name="self"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string LogFormat(this string self, string title = "")
        {
            return $"=={self}=={title}==";
        }
        #endregion
    }
}
