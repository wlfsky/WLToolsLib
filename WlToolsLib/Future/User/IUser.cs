using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WlToolsLib.Future.User
{
    public interface IUser
    {
        /*信息，个人功能，管理功能*/
        UserInfo Regist(UserInfo user);

        UserInfo Login(UserInfo user);

        UserInfo Update(UserInfo user);

        UserInfo ResetPassword(UserInfo user);

        UserInfo Verify(UserInfo user);

        UserInfo GetUser(UserInfo user);

        UserInfo GetUserByLoginName(UserInfo user);

        UserInfo GetUserByMobile(UserInfo user);
        UserInfo GetUserByEmail(UserInfo user);
    }
}

