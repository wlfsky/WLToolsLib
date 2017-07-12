using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.Expand;

namespace WlToolsLib.JsonHelper
{
    #region --json扩展--
    /// <summary>
    /// json扩展方法
    /// </summary>
    public static class JsonExpandFunc
    {
        /// <summary>
        /// 对象转换json, 全转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T self)
        {
            IJsonHelper jh = new NewtonJsonHelper();
            var jsonStr = jh.Serialize<T>(self);
            return jsonStr;
        }

        /// <summary>
        /// 对象转换json，指定字段转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T self, IList<string> showField)
        {
            if (showField.IsNull())
            {
                showField = new List<string>();
            }
            IJsonHelper jh = new NewtonJsonHelper();
            var jsonStr = jh.Serialize<T>(self, showField);
            return jsonStr;
        }

        /// <summary>
        /// 给字段组加入默认值
        /// 注意只能使用在
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IList<string> WithDefaultField(this IList<string> self)
        {
            return self.AddRange(new string[] { "Success", "Data", "Info", "Infos", "Version", "Time", "Code" });
        }

        /// <summary>
        /// json转换对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T ToObj<T>(this string self)
        {
            if (self.NullEmpty())
            {
                return default(T);
            }
            IJsonHelper jh = new NewtonJsonHelper();
            var result = jh.Deserialize<T>(self);
            return result;
        }

        /// <summary>
        /// 从json字符串转换成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T FormJson<T>(this string self)
        {
            if (self.NotNullEmpty())
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(self);
            }
            return default(T);
        }

    }
    #endregion --json扩展--
}
