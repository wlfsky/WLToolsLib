// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    Keccak224Hash
// CreateTime:  2017/07/12 10:59:28
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
    /// Keccak224 加密
    /// </summary>
    public class Keccak224Hash : BaseHashCrypto, ICryptoHelper
    {
        public Keccak224Hash()
            : base("Keccak224")
        {
            currentHash = HashFactory.Crypto.SHA3.CreateKeccak224();
        }
    }
}
