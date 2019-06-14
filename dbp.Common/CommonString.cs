using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAManage.Common
{
    public static class CommonString
    {
        public static string GetStr(int status, string msg)
        {
            string str = string.Empty;
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = string.Format("{\"status\":{0},\"msg\":\"{1}\"", status, msg);
            }
            return str;
        }
    }
}
