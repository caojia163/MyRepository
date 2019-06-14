using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bdp.Models;
using bdp.IDal;
using bdp.Dal;
using bdp.IBll;
using GAManage.Common;

namespace bdp.Bll
{
    /// <summary>
    /// 登录信息业务管理服务
    /// </summary>
    public class LoginManage: ILoginManage
    {
        ILoginServices loginServices = new LoginServices();

        public bool IsCheckUserName(string userName)
        {
            try
            {
                return loginServices.IsCheckUserName(userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel GetSysUser(string userName, string passWord)
        {
            try
            {
                return loginServices.GetSysUser(userName, passWord);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
