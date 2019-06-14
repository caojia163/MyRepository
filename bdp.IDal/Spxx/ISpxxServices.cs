using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bdp.Models.Spxx;

namespace bdp.IDal.Spxx
{
    /// <summary>
    /// 商品信息数据服务接口
    /// </summary>
    public interface ISpxxServices
    {
        //添加商品信息
        bool AddSpxx(SpxxModel model);

        //获取商品信息列表
        DataTable GetList(SpxxReq model);

        //获取记录总数
        int GetRecordCount(SpxxReq model);

        //分页获取数据列表
        DataTable GetListByPage(SpxxReq model, string orderby, int rows, int page);

        //获取字典信息列表
        DataTable GetDropDownList(string key);
        //根据ID获取商品信息
        SpxxModel GetSpxxDetailById(string spId);
    }
}
