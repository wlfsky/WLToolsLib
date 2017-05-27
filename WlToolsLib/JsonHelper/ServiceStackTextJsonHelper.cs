using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace WlToolsLib.JsonHelper
{
    #region --ServiceStackText的json序列化，带有时间转化--
    /// <summary>
    /// 据说堪比亚光速json转换
    /// 还有一种光速的没有写。
    /// </summary>
    public class ServiceStackTextJsonHelper : IJsonHelper
    {
        public ServiceStackTextJsonHelper()
        {
        }

        public string Serialize(object obj)
        {
            //用ISO方式格式化
            //ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.DateHandler.ISO8601;
            //自定义格式化
            JsConfig<DateTime>.SerializeFn = time => time.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return JsonSerializer.SerializeToString(obj, obj.GetType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize<T>(T obj)
        {
            //用ISO方式格式化
            //ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.DateHandler.ISO8601;
            //自定义格式化
            JsConfig<DateTime>.SerializeFn = time => time.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return JsonSerializer.SerializeToString(obj, obj.GetType());
        }

        public string Serialize<TType>(TType j_data, IList<string> ignoreFields)
        {
            return "";
        }

        public T Deserialize<T>(string jsonStr)
        {
            //用ISO方式格式化
            //ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.DateHandler.ISO8601;
            //自定义格式化
            JsConfig<DateTime>.DeSerializeFn = timeStr =>
            {
                System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss.fff";
                return Convert.ToDateTime(timeStr, dtFormat);
            };
            return JsonSerializer.DeserializeFromString<T>(jsonStr);
        }
    }
    #endregion
}
