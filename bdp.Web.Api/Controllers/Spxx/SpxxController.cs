using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using bdp.IBll.Spxx;
using bdp.Bll.Spxx;
using bdp.Models;
using bdp.Models.Spxx;
using GAManage.Common;


namespace bdp.Web.Api.Controllers.Spxx
{
    public class SpxxController : ApiController
    {
        ISpxxManage spxxManage = new SpxxManage();

        #region 查询商品信息列表
        [HttpPost]
        public string QuerySpxxList([FromBody]SpxxReq req)
        {
            Result<DataTable> result = new Result<DataTable>();
            try
            {
                result.status = 1;
                result.data = spxxManage.GetList(req);
            }
            catch (Exception ex)
            {
                result.status = 0;
                result.msg = "信息异常!查询商品信息：" + ex.Message;
                LogHelper.WriteLog(this.GetType(), "查询部门信息列表出错了！错误信息：" + ex.Message, LogHelper.logLevel.ERROR);
            }
            return JsonHelper.ConvertToJosnString(result);
        }
        #endregion

        #region 分页查询商品信息列表
        [HttpPost]
        public string QuerySpxxListByPage([FromBody]SpxxReq req)
        {
            Result<ResultData<SpxxRes>> result = new Result<ResultData<SpxxRes>>();
            try
            {
                DataTable dt = spxxManage.GetListByPage(req, string.Empty, req.rows, req.page);
                ResultData<SpxxRes> data = new ResultData<SpxxRes>();
                data.total = spxxManage.GetRecordCount(req);
                data.rows = ModelEx.SetListValueFromDB<SpxxRes>(new List<SpxxRes>(), dt);
                result.status = 1;
                result.data = data;
            }
            catch (Exception ex)
            {
                result.status = 0;
                result.msg = "信息异常!查询商品信息：" + ex.Message;
                LogHelper.WriteLog(this.GetType(), "查询部门信息列表出错了！错误信息：" + ex.Message, LogHelper.logLevel.ERROR);
            }
            return JsonHelper.ConvertToJosnString(result);
        }
        #endregion

        #region 查询商品信息列表
        [HttpGet]
        public string GetSpxxById(string Id)
        {
            Result<SpxxModel> result = new Result<SpxxModel>();
            try
            {
                result.status = 1;
                result.data = spxxManage.GetSpxxDetailById(Id);
            }
            catch (Exception ex)
            {
                result.status = 0;
                result.msg = "信息异常!查询单个商品信息：" + ex.Message;
                LogHelper.WriteLog(this.GetType(), "查询单个商品信息出错了！错误信息：" + ex.Message, LogHelper.logLevel.ERROR);
            }
            return JsonHelper.ConvertToJosnString(result);
        }
        #endregion


        #region 商品信息新增服务
        // POST: api/Spxx
        [HttpPost]
        public string AddSpxx([FromBody]SpxxModel model)
        {
            Result<string> result = new Result<string>();
            try
            {
                if (spxxManage.AddSpxx(model))
                {
                    result.status = 1;
                    result.msg = "添加成功！";
                }
                else
                {
                    result.status = 0;
                    result.msg = "添加失败！";
                }
            }
            catch (Exception ex)
            {
                result.status = 0;
                result.msg = "信息异常！商品信息新增：" + ex.Message;
                LogHelper.WriteLog(this.GetType(), "商品信息新增出错了！错误信息：" + ex.Message, LogHelper.logLevel.ERROR);
            }
            return JsonHelper.ConvertToJosnString(result);
        }
        #endregion

       

        #region 获取下拉列表
        [HttpGet]
        public string GetDropDownList(string key)
        {
            Result<DataTable> result = new Result<DataTable>();
            try
            {
                result.status = 1;
                result.data = spxxManage.GetDropDownList(key);
            }
            catch (Exception ex)
            {
                result.status = 0;
                result.msg = "信息异常!获取下拉列表：" + ex.Message;
                LogHelper.WriteLog(this.GetType(), "获取下拉列表出错了！错误信息：" + ex.Message, LogHelper.logLevel.ERROR);
            }
            return JsonHelper.ConvertToJosnString(result);
        }
        #endregion
    }
}
