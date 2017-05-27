using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

namespace WlToolsLib.DBHelper
{
    public class MSSQLPowerDBHelper : PowerDBHelper, IPowerDBHelper
    {
        public MSSQLPowerDBHelper(string connStr, int comTimeOut) : base(connStr, comTimeOut, null)
        {
            ConnectionMaker = (connstr) => { return new SqlConnection(connstr); };
        }

        public void AddParameter(DbParameterCollection comPara, string paramName, object paramValue)
        {
            comPara.Add(new SqlParameter(paramName, paramValue));
        }
        

    }
}
