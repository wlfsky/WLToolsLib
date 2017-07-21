// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    MD5Hash
// CreateTime:  2017/07/12 10:50:54
// Creator:     weilai
// FileRemark:  
// ------------------------------------


namespace WlToolsLib.CryptoHelper
{
    using HashLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    /// MD5散列加密
    /// </summary>
    public class MD5Hash : BaseHashCrypto, ICryptoHelper
    {
        public MD5Hash()
            : base("MD5")
        {
            currentHash = HashFactory.Crypto.CreateMD5();
        }

    }
}
