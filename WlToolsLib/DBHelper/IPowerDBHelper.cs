using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using WlToolsLib.DataShell;

namespace WlToolsLib.DBHelper
{
    /// <summary>
    /// 数据库基础接口。实际上不推荐用这个，还是用dapper之类理想
    /// </summary>
    public interface IPowerDBHelper
    {
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="comPara"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        void AddParameter(DbParameterCollection comPara, string paramName, object paramValue);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="parameterMaker"></param>
        /// <returns></returns>
        DataShell<int> ExecuteSql(string sqlStr, Action<DbParameterCollection> parameterMaker);

        /// <summary>
        /// 读取单个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="parameterMaker"></param>
        /// <param name="dataConverter"></param>
        /// <returns></returns>
        DataShell<T> SingleDataReader<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<DbDataReader, T> dataConverter);

        /// <summary>
        /// 读取首行首个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="parameterMaker"></param>
        /// <param name="dataConverter"></param>
        /// <returns></returns>
        DataShell<T> ExecuteScalar<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<object, T> dataConverter);
        /// <summary>
        /// 读取数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="parameterMaker"></param>
        /// <param name="dataConverter"></param>
        /// <returns></returns>
        DataShell<List<T>> ListDataReader<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<DbDataReader, T> dataConverter);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterMaker"></param>
        /// <param name="dataConverter"></param>
        /// <returns></returns>
        DataShell<T> ExecuteStoredProcedure<T>(string storedProcedureName, Action<DbParameterCollection> parameterMaker, Func<DbParameterCollection, T> dataConverter);
    }
}
