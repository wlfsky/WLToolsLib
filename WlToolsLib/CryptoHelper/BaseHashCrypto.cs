using HashLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.CryptoHelper
{
    /// <summary>
    /// hash加密基类，解密直接返回输入值
    /// 只需复写加密类
    /// </summary>
    public abstract class BaseHashCrypto : ICryptoHelper
    {
        /// <summary>
        /// 初始化基本加密类，用utf8编码
        /// </summary>
        /// <param name="name"></param>
        public BaseHashCrypto(string name)
            : this(name, Encoding.UTF8)
        {
        }

        /// <summary>
        /// 初始化基本加密类，自定义编码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="encode"></param>
        public BaseHashCrypto(string name, Encoding encode)
        {
            DecryptName = name;
            Encode = encode;
        }

        /// <summary>
        /// 编码方式，默认是utf8
        /// </summary>
        public Encoding Encode { get; set; }

        /// <summary>
        /// 加密名称
        /// </summary>
        public string DecryptName { get; protected set; }
        
        /// <summary>
        /// 加密名称
        /// </summary>
        public string CryptoName { get; protected set; }

        /// <summary>
        /// 是否小写
        /// </summary>
        public bool IsLower { get; set; }

        protected IHash currentHash;

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public virtual string Encryption(string sourceStr)
        {
            if (string.IsNullOrWhiteSpace(sourceStr)) throw new NothingToEncrypOrDecryptException();
            HashResult result = currentHash.ComputeString(sourceStr, Encode);
            string resultStr = result.ToString();
            return ResultFilter(resultStr);
        }

        /// <summary>
        /// 解密，对于hash加密  解密直接返回值
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public virtual string Decryption(string sourceStr)
        {
            if (string.IsNullOrWhiteSpace(sourceStr)) throw new NothingToEncrypOrDecryptException();
            return sourceStr;
        }

        /// <summary>
        /// 过滤结果，去掉 “-” 
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public string ResultFilter(string sourceStr)
        {
            string result = sourceStr.Replace("-", "");
            if (IsLower == true)
                result = result.ToLower();
            return result;
        }
    }
}
