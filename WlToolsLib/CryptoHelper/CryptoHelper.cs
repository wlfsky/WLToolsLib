/*
 * 加密类，hash加密 和 对称加密，非对禅加密都 组合在一起。只是hash加密后解密方法直接返回hash原文。
 * 初定配置 MD5， SHA1  SHA128 SHA256 和 Keccak（最高级散列加密）
 * 以后考虑集成 谷歌反 量子解密 的加密算法
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashLib;

namespace WlToolsLib.CryptoHelper
{
    /// <summary>
    /// 加密或者解密源 无数据 异常
    /// </summary>
    public class NothingToEncrypOrDecryptException : Exception
    {
        public NothingToEncrypOrDecryptException()
            : base("没有数据可供加密或者解密")
        {
        }
    }
}
