using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bdp.IBll.Spxx;
using bdp.IDal.Spxx;
using bdp.Dal.Spxx;
using bdp.Models.Spxx;


namespace bdp.Bll.Spxx
{
    /// <summary>
    /// 商品信息业务管理服务
    /// </summary>
    public class SpxxManage : ISpxxManage
    {
        ISpxxServices spxxServices = new SpxxServices();
        public bool AddSpxx(SpxxModel model)
        {
            try
            {
                return spxxServices.AddSpxx(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SpxxModel GetSpxxDetailById(string spId)
        {
            try
            {
                return spxxServices.GetSpxxDetailById(spId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetList(SpxxReq model)
        {
            try
            {
                return spxxServices.GetList(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetRecordCount(SpxxReq model)
        {
            try
            {
                return spxxServices.GetRecordCount(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetListByPage(SpxxReq model, string orderby, int rows, int page)
        {
            try
            {
                return spxxServices.GetListByPage(model, orderby, rows, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDropDownList(string key)
        {
            try
            {
                return spxxServices.GetDropDownList(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
