using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WlToolsLib.DataShell;

namespace WlToolsLib.DBHelper
{
    /// <summary>
    /// 数据库链接类
    /// </summary>
    public abstract class PowerDBHelper
    {
        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="comTimeout"></param>
        /// <param name="connectionMaker"></param>
        protected PowerDBHelper(string connStr, int comTimeout, Func<string, DbConnection> connectionMaker)
        {
            ConnStr = connStr;
            CommTimeOut = comTimeout;
        }

        /// <summary>
        /// 链接生成器
        /// </summary>
        public Func<string, DbConnection> ConnectionMaker { get; protected set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnStr
        {
            get; protected set;
        }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int CommTimeOut
        {
            get; protected set;
        }

        #region --基本数据库方法，委托版本--
        /// <summary>
        /// 默认命令类型
        /// </summary>
        /// <param name="com"></param>
        private void DefaultCommandType(DbCommand com)
        {
            com.CommandType = CommandType.Text;
        }

        /// <summary>
        /// 核心执行sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="parameterMaker"></param>
        /// <param name="executeCommand"></param>
        /// <returns></returns>
        private DataShell<T> CoreExecute<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<DbCommand, T> executeCommand, Action<DbCommand> setCommandType = null
        )
        {
            if (ConnectionMaker == null)
                throw new DbConnException("no connection");
            DataShell<T> result = DataShell<T>.CreateFail<T>();
            setCommandType = setCommandType == null ? DefaultCommandType : setCommandType;
            string sql = sqlStr;
            try
            {
                if(string.IsNullOrWhiteSpace(ConnStr))
                    throw new DbConnException("no connection string");
                using (DbConnection scon = ConnectionMaker(ConnStr))
                {
                    using (DbCommand scom = scon.CreateCommand())
                    {
                        scom.CommandText = sql;
                        setCommandType(scom);
                        scom.CommandTimeout = CommTimeOut;
                        scom.Parameters.Clear();
                        if (parameterMaker != null)
                            parameterMaker(scom.Parameters);
                        scon.Open();
                        if (executeCommand != null)
                        {
                            result.Data = executeCommand(scom);
                            result.Success = true;
                        }
                        scon.Close();
                        scom.Dispose();
                    }
                    scon.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Failed(ex.Message).Failed(ex);
            }
            finally
            {
                
            }
            return result;
        }

        /// <summary>
        /// 执行一般sql语句
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="parameterMaker">参数变量委托</param>
        /// <returns>返回执行影响条数</returns>
        public DataShell<int> ExecuteSql(string sqlStr, Action<DbParameterCollection> parameterMaker)
        {
            return CoreExecute<int>(sqlStr, parameterMaker, (com) => { return com.ExecuteNonQuery(); });
        }

        /// <summary>
        /// 执行单个数据对象提取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="parameterMaker">参数变量委托</param>
        /// <param name="dataConverter"></param>
        /// <returns></returns>
        public DataShell<T> SingleDataReader<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<DbDataReader, T> dataConverter)
        {
            return CoreExecute<T>(sqlStr, parameterMaker, (com) => {
                var data_reader = com.ExecuteReader(CommandBehavior.CloseConnection);
                T t = default(T);
                if (data_reader.Read())
                {
                    if (dataConverter != null)
                        t = dataConverter(data_reader);
                }
                data_reader.Close();
                return t;
            });
        }

        /// <summary>
        /// 执行首行首字段内容提取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="parameterMaker">参数变量委托</param>
        /// <param name="dataConverter"></param>
        /// <returns></returns>
        public DataShell<T> ExecuteScalar<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<object, T> dataConverter)
        {
            return CoreExecute<T>(sqlStr, parameterMaker, (com) => {
                T t = default(T);
                if (dataConverter != null)
                    t = dataConverter(com.ExecuteScalar());
                return t;
            });
        }

        /// <summary>
        /// 执行列表对象提取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="parameterMaker">参数变量委托</param>
        /// <param name="dataConverter"></param>
        /// <returns></returns>
        public DataShell<List<T>> ListDataReader<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<DbDataReader, T> dataConverter)
        {
            return CoreExecute<List<T>>(sqlStr, parameterMaker, (com) => {
                List<T> itemList = new List<T>();
                var data_reader = com.ExecuteReader(CommandBehavior.CloseConnection);
                while (data_reader.Read())
                {
                    T t = default(T);
                    if (dataConverter != null)
                    {
                        t = dataConverter(data_reader);
                        itemList.Add(t);
                    }
                }
                data_reader.Close();
                return itemList;
            });
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterMaker"></param>
        /// <param name="dataConverter"></param>
        /// <returns></returns>
        public DataShell<T> ExecuteStoredProcedure<T>(string storedProcedureName, Action<DbParameterCollection> parameterMaker, Func<DbParameterCollection, T> dataConverter)
        {
            return CoreExecute<T>(storedProcedureName, parameterMaker, (com) => {
                com.ExecuteNonQuery();
                return dataConverter(com.Parameters);
            }, (com)=> { com.CommandType = CommandType.StoredProcedure; });
        }

        /// <summary>
        /// 返回或抛出错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataShell<T> ReturnOrThrowException<T>(DataShell<T> result)
        {
            if (result.Success == true)
                return result;
            else if (result.ExceptionList != null)
                throw new DbConnException(result.Info);
            else
                throw new DbConnException(result.Info);
        }
        #endregion
    }
}
