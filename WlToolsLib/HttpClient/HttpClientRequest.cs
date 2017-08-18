using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WlToolsLib.Expand;
using System.Globalization;
using static System.Console;

namespace WlToolsLib.EasyHttpClient
{
    public class HttpClientRequest
    {

        public Dictionary<string, string> HeaderDic { get; set; }

        /// <summary>
        /// 提交组合数据，
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <param name="form"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> PostMultipart(string url, IDictionary<string, string> header, IDictionary<string, string> form, IDictionary<string, string> file)
        {
            using (var client = new HttpClient())
            {
                var uriStr = url;
                var uri = new Uri(uriStr);
                client.BaseAddress = uri;
                using (var content = new MultipartFormDataContent("----Upload-WlClient----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    if (header.HasItem())
                    {
                        foreach (var item in header)
                        {
                            content.AddHeaderContent(item.Key, item.Value);
                        }
                    }
                    if (form.HasItem())
                    {
                        foreach (var item in form)
                        {
                            content.AddFormContent(item.Key, item.Value);
                        }
                    }
                    if (file.HasItem())
                    {
                        foreach (var item in file)
                        {
                            content.AddFileContent(item.Key, item.Value);
                        }
                    }
                    using (var message = await client.PostAsync(uri, content))
                    {
                        var input = await message.Content.ReadAsStringAsync();
                        return input;
                    }
                }
            }
        }

        /// <summary>
        /// Post发送json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uriStr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task<string> Post<T>(string uriStr, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri(uriStr);
                client.BaseAddress = uri;
                if (HeaderDic.HasItem())
                {
                    foreach (var item in HeaderDic)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                using (var msg = client.PostAsJsonAsync(uri, obj).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()))
                {
                    var r = msg.Result.Content.ReadAsStringAsync();
                    return r;
                }
            }
        }

        public Task<string> Get(string uriStr)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri(uriStr);
                client.BaseAddress = uri;
                if (HeaderDic.HasItem())
                {
                    foreach (var item in HeaderDic)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                using (var msg = client.GetStringAsync(uri).ContinueWith((postTask) => postTask.Result))
                {
                    return msg;
                }
            }
        }

        public Task<string> Put<TIn>(string uriStr, TIn obj)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri(uriStr);
                client.BaseAddress = uri;
                if (HeaderDic.HasItem())
                {
                    foreach (var item in HeaderDic)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                using (var msg = client.PutAsJsonAsync(uri, obj).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()))
                {
                    var r = msg.Result.Content.ReadAsStringAsync();
                    return r;
                }
            }
        }

        public Task<string> Delete(string uriStr)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri(uriStr);
                client.BaseAddress = uri;
                if (HeaderDic.HasItem())
                {
                    foreach (var item in HeaderDic)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                using (var msg = client.DeleteAsync(uri).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()))
                {
                    var r = msg.Result.Content.ReadAsStringAsync();
                    return r;
                }
            }
        }
    }
}