using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bdp.Models
{
    /// <summary>
    /// 用户登录
    /// </summary>
    [Serializable]
    public class UserModel
    {
        #region 私有属性
        private string userId;
        private string userName;
        private string passWord;
        private string realName;
        private int sex;
        private int age;
        private DateTime brithday;
        private string email;
        private DateTime addTime;
        private int isDel;
        #endregion

        #region
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId
        {
            set { userId = value; }
            get { return userId; }
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            set { userName = value; }
            get { return userName; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            set { passWord = value; }
            get { return passWord; }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName
        {
            set { realName = value; }
            get { return realName; }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex
        {
            set { sex = value; }
            get { return sex; }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            set { age = value; }
            get { return age; }
        }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Brithday
        {
            set { brithday = value; }
            get { return brithday; }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            set { email = value; }
            get { return email; }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            set { addTime = value; }
            get { return addTime; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel
        {
            set { isDel = value; }
            get { return isDel; }
        }
        #endregion
    }


}
