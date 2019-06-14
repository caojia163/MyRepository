using bdp.Models;
using GAManage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace bdp.Web.Api.Controllers.UploadFile
{
    public class UploadFileController : ApiController
    {
        #region 上传图片
        [HttpPost]
        public string UploadImages()
        {
            Result<string> result = new Result<string>();
            try
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["file"];
                UploadHelper uh = new UploadHelper();
                result.status = 1;
                result.data = uh.UploadFile(file);
            }
            catch (Exception ex)
            {
                result.status = 0;
                result.msg = "信息异常！上传图片：" + ex.Message;
                LogHelper.WriteLog(this.GetType(), "上传图片出错了！错误信息：" + ex.Message, LogHelper.logLevel.ERROR);
            }
            return JsonHelper.ConvertToJosnString(result);
        }
        #endregion
    }
}
