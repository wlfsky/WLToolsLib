using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WlToolsLib.Future.User
{
    public class UserInfo : BaseRecord
    {
        public string UserID { get; set; }
        public string LoginName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string VerifyPassword { get; set; }
        //
        public int LoginCount { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
