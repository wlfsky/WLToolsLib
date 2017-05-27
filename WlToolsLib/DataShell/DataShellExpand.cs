using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.DataShell
{
    #region --返回结果扩展方法--
    /// <summary>
    /// 结果外壳扩展方法
    /// </summary>
    public static class DataShellExpand
    {
        #region --用返回类型直接构建一个 成功返回结果--

        /// <summary>
        /// 用返回类型直接构建一个 成功返回结果
        /// </summary>
        /// <typeparam name="TResult">结果实体类型</typeparam>
        /// <param name="self">扩展字符串类型</param>
        /// <returns></returns>
        public static DataShell<TResult> Success<TResult>(this TResult self)
        {
            return DataShellCreator.CreateSuccess<TResult>(self);
        }

        #endregion --用返回类型直接构建一个 成功返回结果--

        #region --利用指定类型直接创建一个成功结果并附带一个信息--
        /// <summary>
        /// 利用指定类型直接创建一个成功结果并附带一个信息
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DataShell<TResult> Success<TResult>(this TResult self, string info)
        {
            return DataShellCreator.CreateSuccess<TResult>(self).Succeed(self, info);
        }
        #endregion --利用指定类型直接创建一个成功结果并附带一个信息--

        #region --用错误信息直接构成一个错误返回信息--
        /// <summary>
        /// 用错误信息直接构成一个错误返回信息
        /// </summary>
        /// <typeparam name="TResult">结果数据实体类型</typeparam>
        /// <param name="self">扩展字符串类型</param>
        /// <returns></returns>
        public static DataShell<TResult> Fail<TResult>(this string self)
        {
            return DataShellCreator.CreateFail<TResult>(self);
        }
        #endregion --用错误信息直接构成一个错误返回信息--

        #region --用一组错误信息初始化一个失败结果--

        /// <summary>
        /// 用一组错误信息初始化一个失败结果
        /// </summary>
        /// <typeparam name="TResult">结果实体类型</typeparam>
        /// <param name="self">扩展字符串类型</param>
        /// <returns></returns>
        public static DataShell<TResult> Fail<TResult>(this IList<string> self)
        {
            return DataShellCreator.CreateFail<TResult>(self);
        }

        #endregion --用一组错误信息初始化一个失败结果--
    }

#endregion --返回结果扩展方法--
}
