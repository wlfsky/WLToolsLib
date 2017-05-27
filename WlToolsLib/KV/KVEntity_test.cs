using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.KV
{
    /// <summary>
    /// 测试
    /// </summary>
    public class KVEntity_test
    {
        public static List<KVEntity<int, string>> DoStatus = new List<KVEntity<int, string>>()
        {
            new KVEntity<int, string>() { ID =0, Name ="无"},
            new KVEntity<int, string>() { ID =1, Name ="待"},
            new KVEntity<int, string>() { ID =2, Name ="中"},
            new KVEntity<int, string>() { ID =3, Name ="已"}
        };
        public static List<KVEntity<string, string>> ReDoStatus = new List<KVEntity<string, string>>()
        {
            new KVEntity<string, string>() { ID ="0", Name ="无"},
            new KVEntity<string, string>() { ID ="1", Name ="待"},
            new KVEntity<string, string>() { ID ="2", Name ="中"},
            new KVEntity<string, string>() { ID ="3", Name ="已"}
        };
        public void test()
        {
            var k = DoStatus.K2E(1);
            var v = DoStatus.V2E("已");
            var rk = ReDoStatus.K2E("2");
            var rv = ReDoStatus.V2E("中");
        }
    }
}
