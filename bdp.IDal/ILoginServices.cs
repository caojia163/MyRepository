using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bdp.Models;

namespace bdp.IDal
{
    /// <summary>
    /// 用户登录数据服务接口
    /// </summary>
    public interface ILoginServices
    {
        //验证用户是否存在
        bool IsCheckUserName(string userName);

        //获取单个系统用户
        UserModel GetSysUser(string userName, string passWord);
    }
}
