using HashLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.CryptoHelper
{
    public class AESCBCCrypto : ICryptoHelper
    {
        /// <summary>
        /// 初始化基本加密类，用utf8编码
        /// </summary>
        /// <param name="name"></param>
        public AESCBCCrypto(string name)
            : this(name, Encoding.UTF8)
        {
        }

        /// <summary>
        /// 初始化基本加密类，自定义编码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="encode"></param>
        public AESCBCCrypto(string name, Encoding encode)
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

        public string Key { get; set; }

        public bool IsLower { get; set; }

        protected byte[] saltBytes = new byte[9] { 13, 34, 27, 67, 189, 255, 104, 219, 122 };

        public AESCBCCrypto SetData(string key)
        {
            this.Key = key;
            return this;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public virtual string Encryption(string sourceStr)
        {
            byte[] bytesToBeEncrypted = Encode.GetBytes(sourceStr);
            byte[] passwordBytes = Encode.GetBytes(this.Key);

            //
            byte[] encryptedBytes = null;

            using (var ms = new MemoryStream())
            {
                using (var AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(32);
                    AES.IV = key.GetBytes(16);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }
            byte[] bytesEncrypted = encryptedBytes;

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        /// <summary>
        /// 解密，对于hash加密  解密直接返回值
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public virtual string Decryption(string sourceStr)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(sourceStr);

            byte[] passwordBytes = Encode.GetBytes(this.Key);
            //passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            ///////////////////
            byte[] decryptedBytes = null;

            using (var ms = new MemoryStream())
            {
                using (var AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(32);
                    AES.IV = key.GetBytes(16);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }
            byte[] bytesDecrypted = decryptedBytes;

            string result = Encode.GetString(bytesDecrypted);

            return result;
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
