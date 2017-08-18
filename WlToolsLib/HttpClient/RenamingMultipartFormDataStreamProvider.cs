using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using WlToolsLib.Expand;

namespace WlToolsLib.EasyHttpClient
{
    /// <summary>
    /// 自定义名字的数据流提供者
    /// </summary>
    public class RenamingMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public string Root { get; set; }
        //public Func<FileUpload.PostedFile, string> OnGetLocalFileName { get; set; }
        public Dictionary<string, string> FilesDic { get; set; }

        public RenamingMultipartFormDataStreamProvider(string root)
            : base(root)
        {
            Root = root;
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (FilesDic.IsNull())
            {
                FilesDic = new Dictionary<string, string>();
            }
            var logicName = headers.ContentDisposition.Name;
            var filePath = headers.ContentDisposition.FileName;
            string fileEx = "." + headers.ContentDisposition.FileName.LastIndexOfRight(".");

            var filename = logicName + fileEx;
            FilesDic.Add(logicName, $"{this.Root}\\{filename}");

            return filename;
        }

    }
}