using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
//using Oracle.DataAccess.Client;
using System.Data.SqlClient;

namespace WlToolsLib.DBHelper
{
    public class OraclePowerDBHelper : PowerDBHelper, IPowerDBHelper
    {
        public OraclePowerDBHelper(string connStr, int comTimeOut) : base(connStr, comTimeOut, null)
        {
            ConnectionMaker = (connstr) => {
                return null;//new OracleConnection(connstr);
            };
        }

        public void AddParameter(DbParameterCollection comPara, string paramName, object paramValue)
        {
            //comPara.Add(new OracleParameter(paramName, paramValue));
        }
    }
}
