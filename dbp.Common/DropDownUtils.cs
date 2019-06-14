using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dbp.Common
{
    public static class DropDownUtils
    {
        /// <summary>
        /// 拍卖方式下拉列表值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPMFS(int type)
        {
            string str = string.Empty;
            switch (type)
            {
                case 1:
                    str = "何兰式";
                    break;
                case 2:
                    str = "英格兰式";
                    break;
                case 3:
                    str = "一口价";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 承诺下拉列表值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetCN(int type)
        {
            string str = string.Empty;
            switch (type)
            {
                case 1:
                    str = "到期无条件付款";
                    break;
                case 2:
                    str = "本票不予承兑";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 票据状态下拉列表值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPJZT(int type)
        {
            string str = string.Empty;
            switch (type)
            {
                case 1:
                    str = "背书已签收";
                    break;
                case 2:
                    str = "未背书";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 付款方式下拉列表值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFKFS(int type)
        {
            string str = string.Empty;
            switch (type)
            {
                case 1:
                    str = "微信";
                    break;
                case 2:
                    str = "支付宝";
                    break;
                case 3:
                    str = "银行";
                    break;
            }
            return str;
        }
    }
}
