using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.JsonHelper
{
    #region --newtonjson的序列化和反序列化，带有时间处理--
    /// <summary>
    /// Newton的json转化，实现了IJsonHelper的接口
    /// </summary>
    public class NewtonJsonHelper : IJsonHelper
    {
        List<JsonConverter> converterList;
        public NewtonJsonHelper()
            : this(null)
        {
            converterList = new List<JsonConverter>();
        }

        public NewtonJsonHelper(List<JsonConverter> converters)
        {
            converterList = new List<JsonConverter>();
            if (converters != null && converters.Count > 0)
            {
                converterList.AddRange(converters);
            }
        }

        public NewtonJsonHelper AddConverters(JsonConverter converter)
        {
            if (converter != null)
            {
                converterList.Add(converter);
            }
            return this;
        }

        public NewtonJsonHelper AddDateTimeConverter(string formatStr)
        {
            if (!string.IsNullOrEmpty(formatStr))
            {
                IsoDateTimeConverter jc = new NewtonDateTimeConverter();
                jc.DateTimeFormat = formatStr;
                converterList.Add(jc);
            }
            return this;
        }

        #region --实现接口--
        public string Serialize<T>(T obj)
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            JsonTextWriter jtw = new JsonTextWriter(new StringWriter(sb));
            Newtonsoft.Json.JsonSerializer js = new Newtonsoft.Json.JsonSerializer();
            try
            {
                if (converterList.Count > 0)
                {
                    foreach (var i in converterList)
                    {
                        js.Converters.Add(i);
                    }
                }
                js.Serialize(jtw, obj);
            }
            catch (Exception er)
            {
                jtw.Close();
                throw er;
            }
            jtw.Flush();
            jtw.Close();
            jtw = null;
            return sb.ToString();
        }

        public T Deserialize<T>(string jsonStr)
        {
            //StringBuilder sb = new StringBuilder(jsonStr);
            JsonTextReader jtr = new JsonTextReader(new StringReader(jsonStr));
            Newtonsoft.Json.JsonSerializer js = new Newtonsoft.Json.JsonSerializer();
            T d = default(T);
            try
            {
                if (converterList.Count > 0)
                {
                    foreach (var i in converterList)
                    {
                        js.Converters.Add(i);
                    }
                }
                d = js.Deserialize<T>(jtr);
            }
            catch (Exception er)
            {
                jtr.Close();
                throw er;
            }
            jtr.Close();
            jtr = null;
            return d;
        }

        /// <summary>
        /// 可控 json 输出  接口
        /// </summary>
        /// <typeparam name="TType">需要转换的类型</typeparam>
        /// <param name="jsonData">需要转换的对象</param>
        /// <param name="showFields">需要显示的字段队列</param>
        /// <returns></returns>
        public string Serialize<T>(T objData, IList<string> showFields)
        {
            // 加入默认的需要显示的字段，没有这些字段可能导致外部结构不完整，尤其是Data，关系着外部结构和内部结构的关联点
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new LimitPropsContractResolver(showFields.ToArray());
            return JsonConvert.SerializeObject(objData, jsetting);
        }
        public string Serialize(object obj, string[] transArray)
        {
            return "";
        }

        public T Deserialize<T>(string jsonStr, string[] transArray)
        {
            return default(T);
        }
        #endregion
    }

    
    #endregion
}
