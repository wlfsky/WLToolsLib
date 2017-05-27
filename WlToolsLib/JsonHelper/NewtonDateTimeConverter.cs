using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.JsonHelper
{
    /// <summary>
    /// 自定义的时间序列化
    /// </summary>
    public class NewtonDateTimeConverter : IsoDateTimeConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string tempValue = string.Empty;
            DateTime tempDateTime = new DateTime();
            if (reader.Value != null)
            {
                tempValue = reader.Value.ToString();
            }
            if (!string.IsNullOrEmpty(tempValue))
            {
                tempDateTime = Convert.ToDateTime(tempValue);
            }
            return tempDateTime;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string temp = ((DateTime)value).ToString(base.DateTimeFormat);
            writer.WriteValue(temp);
            //base.WriteJson(writer, temp, serializer);
        }
    }
}
