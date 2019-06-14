using bdp.Models;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GAManage.Common;
using bdp.IDal;

namespace bdp.Dal
{
    /// <summary>
    /// 登录信息数据访问服务
    /// </summary>
    public class LoginServices : ILoginServices
    {
        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsCheckUserName(string userName)
        {
            string strSql = "select count(userName) from tbl_User Where  IsDel <> '1' and UserName = '" + userName + "'";
            try
            {
                return (int)DbHelperSQL.GetSingle(strSql) > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public UserModel GetSysUser(string userName, string passWord)
        {
            string strSql = "select * from tbl_User Where  IsDel <> '1'";
            try
            {
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    strSql += " and UserName = '" + userName + "'";
                }
                if (!string.IsNullOrWhiteSpace(passWord))
                {
                    strSql += " and PassWord = '" + passWord + "'";
                }
                return ModelEx.SetValueFromDB<UserModel>(new UserModel(), DbHelperSQL.Query(strSql).Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
