using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bdp.Models.Spxx;


namespace bdp.IBll.Spxx
{
    /// <summary>
    /// 商品信息业务管理接口
    /// </summary>
    public interface ISpxxManage
    {
        bool AddSpxx(SpxxModel model);

        SpxxModel GetSpxxDetailById(string spId);

        DataTable GetList(SpxxReq model);

        int GetRecordCount(SpxxReq model);

        DataTable GetListByPage(SpxxReq model, string orderby, int rows, int page);

        DataTable GetDropDownList(string key);
    }
}
