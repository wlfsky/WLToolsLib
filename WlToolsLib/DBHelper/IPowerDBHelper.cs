using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using WlToolsLib.DataShell;

namespace WlToolsLib.DBHelper
{
    public interface IPowerDBHelper
    {
        void AddParameter(DbParameterCollection comPara, string paramName, object paramValue);
        DataShell<int> ExecuteSql(string sqlStr, Action<DbParameterCollection> parameterMaker);
        DataShell<T> SingleDataReader<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<DbDataReader, T> dataConverter);
        DataShell<T> ExecuteScalar<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<object, T> dataConverter);
        DataShell<List<T>> ListDataReader<T>(string sqlStr, Action<DbParameterCollection> parameterMaker, Func<DbDataReader, T> dataConverter);
        DataShell<T> ExecuteStoredProcedure<T>(string storedProcedureName, Action<DbParameterCollection> parameterMaker, Func<DbParameterCollection, T> dataConverter);
    }
}
