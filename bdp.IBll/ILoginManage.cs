using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bdp.Models;

namespace bdp.IBll
{
    /// <summary>
    /// 登录信息业务管理接口
    /// </summary>
    public interface ILoginManage
    {
        bool IsCheckUserName(string userName);

        UserModel GetSysUser(string userName, string passWord);
    }
}
