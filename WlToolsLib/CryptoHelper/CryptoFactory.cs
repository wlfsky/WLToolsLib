using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.CryptoHelper
{
    /// <summary>
    /// 散列加密工厂
    /// </summary>
    public class CryptoFactory : ICryptoFactory
    {
        /// <summary>
        /// 静态密码加密创建方法
        /// </summary>
        /// <returns></returns>
        public static ICryptoHelper CreatePasswordCryptoHelper()
        {
            return new CryptoFactory().PasswordCryptoHelper();
        }

        /// <summary>
        /// 密码加密创建法
        /// </summary>
        /// <returns></returns>
        public ICryptoHelper PasswordCryptoHelper()
        {
            return new MD5Hash();
        }
    }
}
