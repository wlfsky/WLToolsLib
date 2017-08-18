using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using WlToolsLib.Expand;

namespace WlToolsLib.EasyHttpClient
{
    /// <summary>
    /// HttpClient 扩展方法
    /// </summary>
    public static class HttpClientExpandFunc
    {
        /// <summary>
        /// 加入一个头内容
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void AddHeaderContent(this MultipartFormDataContent self, string key, string val)
        {
            self.Headers.Add(key, val);
        }

        /// <summary>
        /// 加入一个form内容
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void AddFormContent(this MultipartFormDataContent self, string key, string val)
        {
            self.Add(new StringContent(val, Encoding.UTF8), key);
        }

        /// <summary>
        /// 加入一个文件内容
        /// </summary>
        /// <param name="self"></param>
        /// <param name="name"></param>
        /// <param name="filePath"></param>
        public static void AddFileContent(this MultipartFormDataContent self, string name, string filePath)
        {
            self.Add(new StreamContent(new FileStream(filePath, FileMode.Open)), name, filePath.LastIndexOfRight("\\"));
        }
    }
}