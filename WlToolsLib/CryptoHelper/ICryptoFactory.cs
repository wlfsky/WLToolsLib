using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.CryptoHelper
{
    /// <summary>
    /// 加密工厂接口
    /// </summary>
    public interface ICryptoFactory
    {
        ICryptoHelper PasswordCryptoHelper();
    }
}
