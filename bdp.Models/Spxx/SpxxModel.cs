using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bdp.Models.Spxx
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Serializable]
    public class SpxxModel
    {
        #region 私有属性
        private string spId;
        private string spName;
        private string spHPH;
        private DateTime spCPRQ;
        private DateTime spHPDQR;
        private string spCPRQC;
        private decimal spPJJE;
        private string spCDRQC;
        private int isKZR;
        private int spPJZT;
        private string spSPRQC;
        private int spCPRCN;
        private int spCDRCN;
        private int isBLJ;
        private int spPMFS;
        private string spQPJ;
        private long spJJFD;
        private DateTime spPMKSSJ;
        private DateTime spPMJSSJ;
        private string spMS;
        private int spFKFS;
        private string spUrl;
        private string spBZJYQ;
        private string spPJGY;
        #endregion

        /// <summary>
        /// 商品Id
        /// </summary>
        public string SpId
        {
            set { spId = value; }
            get { return spId; }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string SpName
        {
            set { spName = value; }
            get { return spName; }
        }

        /// <summary>
        /// 汇票号
        /// </summary>
        public string SpHPH
        {
            set { spHPH = value; }
            get { return spHPH; }
        }

        /// <summary>
        /// 出票日期
        /// </summary>
        public DateTime SpCPRQ
        {
            set { spCPRQ = value; }
            get { return spCPRQ; }
        }

        /// <summary>
        /// 汇票到期日
        /// </summary>
        public DateTime SpHPDQR
        {
            set { spHPDQR = value; }
            get { return spHPDQR; }
        }

        /// <summary>
        /// 出票人全称
        /// </summary>
        public string SpCPRQC
        {
            set { spCPRQC = value; }
            get { return spCPRQC; }
        }

        /// <summary>
        /// 票据金额
        /// </summary>
        public decimal SpPJJE
        {
            set { spPJJE = value; }
            get { return spPJJE; }
        }

        /// <summary>
        /// 成兑人全称
        /// </summary>
        public string SpCDRQC
        {
            set { spCDRQC = value; }
            get { return spCDRQC; }
        }

        /// <summary>
        /// 是否可转让
        /// </summary>
        public int IsKZR
        {
            set { isKZR = value; }
            get { return isKZR; }
        }

        /// <summary>
        /// 票据状态
        /// </summary>
        public int SpPJZT
        {
            set { spPJZT = value; }
            get { return spPJZT; }
        }

        /// <summary>
        /// 收票人全称
        /// </summary>
        public string SpSPRQC
        {
            set { spSPRQC = value; }
            get { return spSPRQC; }
        }

        /// <summary>
        /// 出票人承诺
        /// </summary>
        public int SpCPRCN
        {
            set { spCPRCN = value; }
            get { return spCPRCN; }
        }

        /// <summary>
        /// 承兑人承诺
        /// </summary>
        public int SpCDRCN
        {
            set { spCDRCN = value; }
            get { return spCDRCN; }
        }

        /// <summary>
        /// 是否有保留价
        /// </summary>
        public int IsBLJ
        {
            set { isBLJ = value; }
            get { return isBLJ; }
        }

        /// <summary>
        /// 拍卖方式
        /// </summary>
        public int SpPMFS
        {
            set { spPMFS = value; }
            get { return spPMFS; }
        }

        /// <summary>
        /// 起拍价
        /// </summary>
        public string SpQPJ
        {
            set { spQPJ = value; }
            get { return spQPJ; }
        }

        /// <summary>
        /// 加价幅度
        /// </summary>
        public long SpJJFD
        {
            set { spJJFD = value; }
            get { return spJJFD; }

        }

        /// <summary>
        /// 拍卖开始时间
        /// </summary>
        public DateTime SpPMKSSJ
        {
            set { spPMKSSJ = value; }
            get { return spPMKSSJ; }
        }

        /// <summary>
        /// 拍卖结束时间
        /// </summary>
        public DateTime SpPMJSSJ
        {
            set { spPMJSSJ = value; }
            get { return spPMJSSJ; }
        }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string SpMS
        {
            set { spMS = value; }
            get { return spMS; }
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        public int SpFKFS
        {
            set { spFKFS = value; }
            get { return spFKFS; }
        }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string SpUrl
        {
            set { spUrl = value; }
            get { return spUrl; }
        }

        /// <summary>
        /// 保证金要求
        /// </summary>
        public string SpBZJYQ
        {
            set { spBZJYQ = value; }
            get { return spBZJYQ; }
        }

        /// <summary>
        /// 票据概要
        /// </summary>
        public string SpPJGY
        {
            set { spPJGY = value; }
            get { return spPJGY; }
        }
    }
}
