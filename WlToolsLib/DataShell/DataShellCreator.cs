using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.DataShell
{
    #region --返回数据创建器--

    /// <summary>
    /// 返回数据创建器
    /// </summary>
    public class DataShellCreator
    {
        #region --静态初始化--

        /// <summary>
        /// 默认初始化一个成功的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataShell<T> CreateSuccess<T>()
        {
            var t = new DataShell<T>();
            t.Succeed();
            return t;
        }

        /// <summary>
        /// 默认初始化一个成功的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataShell<T> CreateSuccess<T>(T v)
        {
            var t = new DataShell<T>();
            return t.Succeed(v);
        }

        /// <summary>
        /// 默认初始化一个失败的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataShell<T> CreateFail<T>()
        {
            var t = new DataShell<T>();
            t.Failed();
            return t;
        }

        /// <summary>
        /// 默认初始化一个失败的返回值，并写入 info
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DataShell<T> CreateFail<T>(string info)
        {
            var t = new DataShell<T>();
            t.Failed(info);
            return t;
        }

        /// <summary>
        /// 默认初始化一个失败的返回值，并写入 info
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DataShell<T> CreateFail<T>(IList<string> infos)
        {
            var t = new DataShell<T>();
            t.Failed(infos);
            return t;
        }

        #endregion --静态初始化--
    }

    #endregion --返回数据创建器--
}
