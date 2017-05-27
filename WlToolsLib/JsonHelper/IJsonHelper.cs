using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.JsonHelper
{
    #region --json序列化接口--
    /// <summary>
    /// json 序列化，反序列化接口
    /// </summary>
    public interface IJsonHelper
    {
        string Serialize<TType>(TType j_data);
        string Serialize<TType>(TType j_data, IList<string> ignoreFields);
        TType Deserialize<TType>(string json_str);
    }
    #endregion
}
