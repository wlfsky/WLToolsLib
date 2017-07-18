using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WlToolsLib.Future.User
{
    public interface IUserManage
    {
        IList<UserInfo> AllUserList();
        IList<UserInfo> UserList();
        IList<UserInfo> UserPage();
        UserInfo DisableUser(UserInfo user);
        UserInfo DelUser(UserInfo user);
    }
}
