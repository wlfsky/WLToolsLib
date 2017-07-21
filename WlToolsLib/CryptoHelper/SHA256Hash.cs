// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    SHA256Hash
// CreateTime:  2017/07/12 10:58:56
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
    /// SHA256 加密
    /// </summary>
    public class SHA256Hash : BaseHashCrypto, ICryptoHelper
    {
        public SHA256Hash()
            : base("SHA256")
        {
            currentHash = HashFactory.Crypto.CreateSHA256();
        }
    }
}
