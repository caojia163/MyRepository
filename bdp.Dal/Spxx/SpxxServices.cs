using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using bdp.Models.Spxx;
using Maticsoft.DBUtility;
using bdp.IDal.Spxx;
using System.Data.SqlClient;
using GAManage.Common;

namespace bdp.Dal.Spxx
{
    /// <summary>
    /// 商品信息数据访问服务
    /// </summary>
    public class SpxxServices : ISpxxServices
    {
        /// <summary>
        /// 添加商品信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSpxx(SpxxModel model)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("INSERT INTO tbl_Spxx(");
                strSql.Append("spId,");
                strSql.Append("spName,");
                strSql.Append("spHPH,");
                strSql.Append("spCPRQ,");
                strSql.Append("spHPDQR,");
                strSql.Append("spCPRQC,");
                strSql.Append("spPJJE,");
                strSql.Append("spCDRQC,");
                strSql.Append("IsKZR,");
                strSql.Append("spPJZT,");
                strSql.Append("spSPRQC,");
                strSql.Append("spCPRCN,");
                strSql.Append("spCDRCN,");
                strSql.Append("IsBLJ,");
                strSql.Append("spPMFS,");
                strSql.Append("spQPJ,");
                strSql.Append("spJJFD,");
                strSql.Append("spPMKSSJ,");
                strSql.Append("spPMJSSJ,");
                strSql.Append("spMS,");
                strSql.Append("spFKFS,");
                strSql.Append("spUrl,");
                strSql.Append("spBZJYQ,");
                strSql.Append("spPJGY)");
                strSql.Append(" VALUES(");
                strSql.Append("@spId,");
                strSql.Append("@spName,");
                strSql.Append("@spHPH,");
                strSql.Append("@spCPRQ,");
                strSql.Append("@spHPDQR,");
                strSql.Append("@spCPRQC,");
                strSql.Append("@spPJJE,");
                strSql.Append("@spCDRQC,");
                strSql.Append("@IsKZR,");
                strSql.Append("@spPJZT,");
                strSql.Append("@spSPRQC,");
                strSql.Append("@spCPRCN,");
                strSql.Append("@spCDRCN,");
                strSql.Append("@IsBLJ,");
                strSql.Append("@spPMFS,");
                strSql.Append("@spQPJ,");
                strSql.Append("@spJJFD,");
                strSql.Append("@spPMKSSJ,");
                strSql.Append("@spPMJSSJ,");
                strSql.Append("@spMS,");
                strSql.Append("@spFKFS,");
                strSql.Append("@spUrl,");
                strSql.Append("@spBZJYQ,");
                strSql.Append("@spPJGY)");

                #region 参数
                SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@spId", DbType.String)
                    {Value=  System.Guid.NewGuid().ToString().Replace("-", "")},
                    new SqlParameter("@spName", SqlDbType.VarChar,50)
                    {Value=model.SpName},
                    new SqlParameter("@spHPH", SqlDbType.VarChar,50)
                    {Value=model.SpHPH},
                      new SqlParameter("@spCPRQ",SqlDbType.DateTime,8)
                    {Value=model.SpCPRQ},
                    new SqlParameter("@spHPDQR", SqlDbType.DateTime,8)
                    {Value=model.SpHPDQR},
                    new SqlParameter("@spCPRQC",SqlDbType.VarChar,50)
                    {Value=model.SpCPRQC},
                    new SqlParameter("@spPJJE",SqlDbType.Decimal)
                    {Value=model.SpPJJE},
                      new SqlParameter("@spCDRQC", SqlDbType.VarChar,50)
                    {Value=model.SpCDRQC},
                    new SqlParameter("@IsKZR", SqlDbType.Int)
                    {Value=model.IsKZR},
                      new SqlParameter("@spPJZT", SqlDbType.Int)
                    {Value=model.SpPJZT},
                    new SqlParameter("@spSPRQC", SqlDbType.VarChar,50)
                    {Value=model.SpSPRQC},
                    new SqlParameter("@spCPRCN",SqlDbType.Int)
                    {Value=model.SpCPRCN},
                    new SqlParameter("@spCDRCN", SqlDbType.Int)
                    {Value=model.SpCDRCN},
                    new SqlParameter("@IsBLJ", SqlDbType.Int)
                    {Value=model.IsBLJ},
                    new SqlParameter("@spPMFS",SqlDbType.Int)
                    {Value=model.SpPMFS},
                    new SqlParameter("@spQPJ", SqlDbType.VarChar,50)
                    {Value=model.SpQPJ},
                    new SqlParameter("@spJJFD",SqlDbType.Int)
                    {Value=model.SpJJFD},
                    new SqlParameter("@spPMKSSJ",SqlDbType.DateTime,8)
                    {Value=model.SpPMKSSJ},
                    new SqlParameter("@spPMJSSJ",SqlDbType.DateTime,8)
                    {Value=model.SpPMJSSJ},
                    new SqlParameter("@spMS",SqlDbType.VarChar,200)
                    {Value=model.SpMS},
                    new SqlParameter("@spFKFS",SqlDbType.Int)
                    {Value=model.SpFKFS},
                     new SqlParameter("@spUrl",SqlDbType.VarChar,50)
                    {Value=model.SpUrl},
                      new SqlParameter("@spBZJYQ",SqlDbType.VarChar,50)
                    {Value=model.SpBZJYQ},
                    new SqlParameter("@spPJGY",SqlDbType.VarChar,50)
                    {Value=model.SpPJGY}
                    };
                #endregion

                return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters.ToArray()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取单个商品信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SpxxModel GetSpxxDetailById(string spId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  spId,spName,spHPH,spCPRQ,spCPRQC,spHPDQR,spPJJE,spCDRQC,IsKZR,spPJZT,spSPRQC,spCPRCN,spCDRCN,IsBLJ,spPMFS,spQPJ,spJJFD,spPMKSSJ,spPMJSSJ,spMS,spFKFS,spUrl,spBZJYQ,spPJGY,AddTime,IsDel from tbl_Spxx ");
                strSql.Append(" where spId=@spId and IsDel <> '1'");
                SqlParameter[] parameters = {
                    new SqlParameter("@spId", SqlDbType.Char,32) };
                parameters[0].Value = spId;

                SpxxModel model = new SpxxModel();
                DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ModelEx.SetValueFromDB<SpxxModel>(model,ds.Tables[0]);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得商品信息数据列表
        /// </summary>
        public DataTable GetList(SpxxReq model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select");
                strSql.Append(" spId,");
                strSql.Append(" spName,");
                strSql.Append(" spHPH,");
                strSql.Append(" spCPRQ,");
                strSql.Append(" spHPDQR,");
                strSql.Append(" spCPRQC,");
                strSql.Append(" IsKZR,");
                strSql.Append(" spPJZT,");
                strSql.Append(" spSPRQC,");
                strSql.Append(" spCPRCN,");
                strSql.Append(" spCDRCN,");
                strSql.Append(" IsBLJ,");
                strSql.Append(" spPMFS,");
                strSql.Append(" spQPJ,");
                strSql.Append(" spJJFD,");
                strSql.Append(" spPMKSSJ,");
                strSql.Append(" spPMJSSJ,");
                strSql.Append(" spMS,");
                strSql.Append(" spFKFS,");
                strSql.Append(" spUrl,");
                strSql.Append(" spBZJYQ,");
                strSql.Append(" spPJGY from  tbl_Spxx ");
                return DbHelperSQL.Query(strSql.ToString()).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(SpxxReq model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(1) FROM tbl_Spxx ");
                //if (strWhere.Trim() != "")
                //{
                //    strSql.Append(" where " + strWhere);
                //}
                object obj = DbHelperSQL.GetSingle(strSql.ToString());
                return obj == null ? 0 : Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetListByPage(SpxxReq model, string orderby, int rows, int page)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER (");
                if (!string.IsNullOrEmpty(orderby.Trim()))
                {
                    strSql.Append("order by T." + orderby);
                }
                else
                {
                    strSql.Append("order by T.spId desc");
                }
                strSql.Append(")AS RowId, T.*  from tbl_Spxx T ");
                //if (!string.IsNullOrEmpty(strWhere.Trim()))
                //{
                //    strSql.Append(" WHERE " + strWhere);
                //}
                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.RowId between {0} and {1}", rows * (page - 1), rows);
                //strSql.AppendFormat("select * from tbl_spxx limit {0},{1}", rows * (page - 1), rows);
                return DbHelperSQL.Query(strSql.ToString()).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 查询字典表数据项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable GetDropDownList(string key)
        {
            try
            {
                string strSql = "select DictCode,DictName from tbl_Dictionary where DictCode like '%" + key + "%' order by DictOrder";
                return DbHelperSQL.Query(strSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
