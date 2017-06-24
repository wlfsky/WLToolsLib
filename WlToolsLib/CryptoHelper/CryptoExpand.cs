using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
