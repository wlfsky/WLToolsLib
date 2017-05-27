#region --文件说明 code remark--
//-----------------------------------------------------------------------
// <copyright file="JsonHelper.cs" company="Slwy Enterprises">
//      Copyright (c) Slwy Enterprises. All rights reserved.
// </copyright>
// <date>2016/10/25</date>
// <Author>weilai</Author>
// <remark>以前在部门业务 代码文件中，现在已经移至Slwy.UserData.Common项目中，而且做了升级，可通过外部提供的显示字段队列 序列化指定项</remark>
//----------------------------------------------------------------------- 
#endregion
using Slwy.Newtonsoft.Json;
using Slwy.Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slwy.UserData.Common
{
    #region --json序列化接口--
    /// <summary>
    /// json 序列化，反序列化接口
    /// </summary>
    public interface IJsonHelper
    {
        string Serialize<TType>(TType j_data);
        string Serialize<TType>(TType j_data, List<string> ignoreFields);
        TType Deserialize<TType>(string json_str);
    }

    /// <summary>
    /// 实例化的 json 序列化和反序列化接口，用newton json
    /// </summary>
    public class NewTonJson : IJsonHelper
    {
        public string Serialize<TType>(TType jsonData)
        {
            return JsonConvert.SerializeObject(jsonData);
        }

        /// <summary>
        /// 可控 json 输出  接口
        /// </summary>
        /// <typeparam name="TType">需要转换的类型</typeparam>
        /// <param name="jsonData">需要转换的对象</param>
        /// <param name="showFields">需要显示的字段队列（已经包含 数据外壳结构 { "Success", "Data", "Info", "Version", "Time", "Code" }）</param>
        /// <returns></returns>
        public string Serialize<TType>(TType jsonData, List<string> showFields)
        {
            ////加入默认的需要显示的字段，没有这些字段可能导致外部结构不完整，尤其是Data，关系着外部结构和内部结构的关联点
            showFields.AddRange(new string[] { "Success", "Data", "Info", "Version", "Time", "Code" });
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new LimitPropsContractResolver(showFields.ToArray());
            return JsonConvert.SerializeObject(jsonData, jsetting);
        }

        public TType Deserialize<TType>(string json_str)
        {
            return JsonConvert.DeserializeObject<TType>(json_str);
        }
    }

    /// <summary>
    /// 限制 字段输出 专门给 可控 json输出使用
    /// </summary>
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] props = null;
        bool retain;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="props">传入的属性数组</param>
        /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
        public LimitPropsContractResolver(string[] props, bool retain = true)
        {
            //指定要序列化属性的清单
            this.props = props;
            this.retain = retain;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list =
            base.CreateProperties(type, memberSerialization);
            //只保留清单有列出的属性
            return list.Where(p =>
            {
                if (retain)
                {
                    return props.Contains(p.PropertyName);
                }
                else
                {
                    return !props.Contains(p.PropertyName);
                }
            }).ToList();
        }
    }
    #endregion
}
