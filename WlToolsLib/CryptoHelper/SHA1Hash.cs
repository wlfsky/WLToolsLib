// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    SHA1Hash
// CreateTime:  2017/07/12 10:58:16
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
    /// SHA1 加密
    /// </summary>
    public class SHA1Hash : BaseHashCrypto, ICryptoHelper
    {
        public SHA1Hash()
            : base("SHA1")
        {
            currentHash = HashFactory.Crypto.CreateSHA1();
        }
    }
}
