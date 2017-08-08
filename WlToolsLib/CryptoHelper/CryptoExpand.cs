using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.Expand;

namespace WlToolsLib.CryptoHelper
{
    public static class CryptoExpand
    {
        #region --加密解密扩展--
        public static string AESCBCEncryption(this string self, string key)
        {
            ICryptoHelper c = new AESCBCCrypto("AESCBC").SetData(key);
            return c.Encryption(self);
        }

        public static string AESCBCDecryption(this string self, string key)
        {
            ICryptoHelper c = new AESCBCCrypto("AESCBC").SetData(key);
            return c.Decryption(self);
        }
        #endregion

        #region --Hash扩展--
        /// <summary>
        /// 转换md5
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToMd5(this string self)
        {
            if (self.NotNullEmpty())
            {
                ICryptoHelper ch = new MD5Hash();
                return ch.Encryption(self);
            }
            return self;
        }

        /// <summary>
        /// 转换SHA1
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToSHA1(this string self)
        {
            if (self.NotNullEmpty())
            {
                ICryptoHelper ch = new SHA1Hash();
                return ch.Encryption(self);
            }
            return self;
        }

        /// <summary>
        /// 转换SHA256
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToSHA256(this string self)
        {
            if (self.NotNullEmpty())
            {
                ICryptoHelper ch = new SHA256Hash();
                return ch.Encryption(self);
            }
            return self;
        }

        /// <summary>
        /// 转换Keccak224
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToKeccak224(this string self)
        {
            if (self.NotNullEmpty())
            {
                ICryptoHelper ch = new Keccak224Hash();
                return ch.Encryption(self);
            }
            return self;
        }

        #endregion
    }
}
