//-----------------------------------------------------------------------
// <copyright file="ResultData.cs" company="Slwy Enterprises">
//     Copyright (c) Slwy Enterprises. All rights reserved.
// </copyright>
// <author>魏莱 20161009</author>
//-----------------------------------------------------------------------
using System;

namespace Slwy.UserData.Common
{
    #region --包装返回类型--
    /// <summary>
    /// 实验功能
    /// 泛型返回数据，用于包装返回数据
    /// 内设置Success是否成功，时间，信息，版本，泛型返回数据
    /// 增加多个成功，和失败设置，统一设置失败和成功的规则
    /// </summary>
    /// <typeparam name="TResult">具体返回类型</typeparam>
    public class ResultData<TResult>
    {
        #region --初始化函数组--
        /// <summary>
        /// 初始化 <see cref="{TResult}"/> 类的新实例。
        /// 时间版本自动设置
        /// </summary>
        public ResultData()
        {
            this.Time = DateTime.Now;
            this.Version = "0.1";
            this.Info = string.Empty;
            this.Code = 0;
        }

        /// <summary>
        /// 初始化，时间和版本自动设置
        /// </summary>
        /// <param name="successs">是否成功</param>
        /// <param name="r_data">返回数据</param>
        /// <param name="info">信息，无信息可置空</param>
        public ResultData(bool isSuccess, TResult r_data, string info)
            : this()
        {
            this.Success = isSuccess;
            this.Data = r_data;
        }

        /// <summary>
        /// 初始化，时间版本自动设置
        /// </summary>
        /// <param name="successs"></param>
        /// <param name="r_data"></param>
        public ResultData(bool success, TResult r_data)
            : this(success, r_data, string.Empty)
        {
        } 
        #endregion

        #region --静态初始化--
        /// <summary>
        /// 默认初始化一个成功的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ResultData<T> InitDefaultSucceed<T>()
        {
            var t = new ResultData<T>();
            t.Succeed();
            return t;
        }

        /// <summary>
        /// 默认初始化一个失败的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ResultData<T> InitDefaultFailed<T>()
        {
            var t = new ResultData<T>();
            t.Failed();
            return t;
        }

        /// <summary>
        /// 默认初始化一个失败的返回值，并写入 info
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public static ResultData<T> InitDefaultFailed<T>(string info)
        {
            var t = new ResultData<T>();
            t.Failed(info);
            return t;
        }
        #endregion

        #region --属性组--
        /// <summary>
        /// 获取或设置一个值，该值指示-执行是否成功(true, false)
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 代码，当前后端约定代码信息后可使用
        /// 默认 0 表示：无意义
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示-信息，如果出错返回错误信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 获取一个值，该值指示-结果时间
        /// </summary>
        public DateTime Time { get; protected set; }

        /// <summary>
        /// 获取一个值，该值指示-返回版本 目前默认 0.1
        /// </summary>
        public string Version { get; protected set; }

        /// <summary>
        /// 获取或设置一个值，该值指示-返回数据
        /// </summary>
        public TResult Data { get; set; }
        #endregion

        #region --方法--
        /// <summary>
        /// 统一设置成功业务
        /// </summary>
        public ResultData<TResult> Succeed()
        {
            this.Success = true;
            this.Code = 1;
            return this;
        }

        /// <summary>
        /// 统一设置成功业务，成功并赋值
        /// </summary>
        /// <param name="data"></param>
        public ResultData<TResult> Succeed(TResult data)
        {
            this.Succeed();
            this.Data = data;
            return this;
        }

        /// <summary>
        /// 统一设置失败业务
        /// </summary>
        public ResultData<TResult> Failed()
        {
            this.Success = false;
            this.Code = -1;
            return this;
        }

        /// <summary>
        /// 统一设置失败业务，带入错误信息
        /// </summary>
        /// <param name="errorInfo"></param>
        public ResultData<TResult> Failed(string errorInfo)
        {
            this.Failed();
            this.Info = errorInfo;
            return this;
        }

        /// <summary>
        /// 统一设置失败业务，带入错误信息，只返回基本错误信息
        /// </summary>
        /// <param name="error"></param>
        public ResultData<TResult> Failed(Exception error)
        {
            this.Failed();
            this.Info = error.Message;
            return this;
        }

        /// <summary>
        /// 统一设置失败业务，带入错误信息，可自定义错误信息处理委托
        /// </summary>
        /// <param name="error"></param>
        public ResultData<TResult> Failed(Exception error, Func<Exception, string> errorInfoMaker)
        {
            this.Failed();
            this.Info = errorInfoMaker(error);
            return this;
        }
        #endregion
    }
    #endregion

    #region --返回数据创建器--
    /// <summary>
    /// 返回数据创建器
    /// </summary>
    public class ResultDataCreator
    {
        #region --静态初始化--
        /// <summary>
        /// 默认初始化一个成功的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ResultData<T> InitDefaultSucceed<T>()
        {
            var t = new ResultData<T>();
            t.Succeed();
            return t;
        }

        /// <summary>
        /// 默认初始化一个成功的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ResultData<T> InitDefaultSucceed<T>(T v)
        {
            var t = new ResultData<T>();
            return t.Succeed(v);
        }

        /// <summary>
        /// 默认初始化一个失败的返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ResultData<T> InitDefaultFailed<T>()
        {
            var t = new ResultData<T>();
            t.Failed();
            return t;
        }

        /// <summary>
        /// 默认初始化一个失败的返回值，并写入 info
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public static ResultData<T> InitDefaultFailed<T>(string info)
        {
            var t = new ResultData<T>();
            t.Failed(info);
            return t;
        }
        #endregion
    }
    #endregion
}
