using bdp.Bll;
using bdp.IBll;
using bdp.Models;
using dbp.Common;
using GAManage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bdp.Web.Api.Controllers
{
    public class LoginController : ApiController
    {
        ILoginManage loginManage = new LoginManage();

        #region 用户登录
        [HttpPost]
        public string UserLogin([FromBody]dynamic jsonObj)
        {
            Result<UserModel> result = new Result<UserModel>();
            if (jsonObj == null)
            {
                result.status = 1;
                result.msg = "用户信息为空,请输入用户信息";
                return JsonHelper.ConvertToJosnString(result);
            }
            try
            {
                string strUserName = Convert.ToString(jsonObj.UserName);
                string strPassWord = Convert.ToString(jsonObj.PassWord);
                string pwd = md5.MD5Encrypt(strPassWord, md5.GetKey());
               
                if (loginManage.IsCheckUserName(strUserName))
                {
                    UserModel user = loginManage.GetSysUser(strUserName, pwd);
                    if (user != null)
                    {
                        result.status = 1;
                        result.data = user;
                    }
                    else
                    {
                        result.status = 0;
                        result.msg = "用户名或密码错误";
                        return JsonHelper.ConvertToJosnString(result);
                    }
                }
                else
                {
                    result.status = 0;
                    result.msg = "该用户名不存在";
                    return JsonHelper.ConvertToJosnString(result);
                }
            }
            catch (Exception ex)
            {
                result.status = 0;
                result.msg = "信息异常!用户登录信息：" + ex.Message;
                LogHelper.WriteLog(this.GetType(), "用户登录信息出错了！错误信息：" + ex.Message, LogHelper.logLevel.ERROR);
            }
            return JsonHelper.ConvertToJosnString(result);
        }
    }
    #endregion
}