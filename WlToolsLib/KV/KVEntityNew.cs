using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.KV
{
    public static class KVEntityNew
    {
        #region --新设计的状态对照 创意代码 不参与实用--
        /// <summary>
        /// 用enum ,代码 ,  名字 对照表
        /// </summary>
        public static List<CodeNameMap<CodeEnum, string, string>> TrainTicketStatusX = new List<CodeNameMap<CodeEnum, string, string>>()
        {
            new CodeNameMap<CodeEnum, string, string>() {  Enum = CodeEnum.Booking, Code ="99", Name ="名字"}
        };

        /// <summary>
        /// 另一个 enum ,代码 ,  名字 对照表
        /// </summary>
        public static List<CodeNameMap<CodeEnum, int, string>> TrainTicketSubStatusX = new List<CodeNameMap<CodeEnum, int, string>>()
        {
            new CodeNameMap<CodeEnum, int, string>() {  Enum = CodeEnum.Booking, Code =0, Name ="预订中"}
        };

        /// <summary>
        /// 对照单元
        /// </summary>
        /// <typeparam name="TCodeEnum"></typeparam>
        /// <typeparam name="TCode"></typeparam>
        /// <typeparam name="TName"></typeparam>
        public class CodeNameMap<TCodeEnum, TCode, TName>
        {
            public TCodeEnum Enum { get; set; }
            public TCode Code { get; set; }
            public TName Name { get; set; }
        }

        /// <summary>
        /// 枚举
        /// </summary>
        public enum CodeEnum { Booking }

        /// <summary>
        /// 查找器 枚举到实体
        /// </summary>
        /// <typeparam name="TCodeEnum"></typeparam>
        /// <typeparam name="TCode"></typeparam>
        /// <typeparam name="TName"></typeparam>
        /// <param name="_self"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static CodeNameMap<TCodeEnum, TCode, TName> E2E<TCodeEnum, TCode, TName>(this List<CodeNameMap<TCodeEnum, TCode, TName>> _self, TCodeEnum k)
        {
            return _self.FirstOrDefault((s) => { return s.Enum.Equals(k); });
        }
        /// <summary>
        /// 范例
        /// </summary>
        /// <param name="ce"></param>
        /// <returns></returns>
        public static CodeNameMap<CodeEnum, string, string> E2E(CodeEnum ce)
        {
            var result = TrainTicketStatusX.E2E(CodeEnum.Booking);
            return result;
        }
        #endregion
    }
}
