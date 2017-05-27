using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.CryptoHelper
{
    /// <summary>
    /// 加密解密接口
    /// </summary>
    public interface ICryptoHelper
    {
        string CryptoName { get; }
        bool IsLower { get; set; }
        Encoding Encode { get; set; }
        string Encryption(string sourceStr);
        string Decryption(string sourceStr);
    }
}
