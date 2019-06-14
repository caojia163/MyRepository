using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace GAManage.Common
{
    /// <summary>
    /// 上传文件帮助类
    /// </summary>
    public class UploadHelper
    {
        /// <summary>
        /// 上传文本文件
        /// </summary>
        /// <param name="base64Code">base64字符串</param>
        public string Upload(string base64Code)
        {
            try
            {
                //获取文件储存路径
                string datetime = GetTimeStamp();
                string suffix = ".txt"; //文件的后缀名根据实际情况
                string filename = datetime + suffix; //文件名称
                string strDir = System.Web.HttpContext.Current.Server.MapPath("~/") + "Upload/Images/";
                string strPath = strDir + filename;
                if (!Directory.Exists(strDir)) //如果不存在就创建文件夹
                {
                    Directory.CreateDirectory(strDir);
                }
                FileStream fs = new FileStream(strPath, FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(base64Code);
                sw.Close();
                fs.Close();

                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="text">base64字符串</param>
        public string UploadImage(string text)
        {
            try
            {
                //获取文件储存路径
                string datetime = GetTimeStamp();
                string suffix = ".jpg"; //文件的后缀名根据实际情况
                string filename = datetime + suffix; //文件名称
                string strDir = System.Web.HttpContext.Current.Server.MapPath("~/") + "Upload/Images/";
                string strPath = strDir + filename;
                if (!Directory.Exists(strDir)) //如果不存在就创建文件夹
                {
                    Directory.CreateDirectory(strDir);
                }
                Base64ToImg(text).Save(strPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name=""></param>
        public string UploadFile(HttpPostedFile file)
        {
            try
            {
                //获取文件储存路径
                string datetime = System.Guid.NewGuid().ToString().Replace("-", "");
                string suffix = ".jpg"; //文件的后缀名根据实际情况
                string filename = datetime + suffix; //文件名称
                string strDir = System.Web.HttpContext.Current.Server.MapPath("~/") + "Upload/Images/";
                string strPath = strDir + filename;
                if (!Directory.Exists(strDir)) //如果不存在就创建文件夹
                {
                    Directory.CreateDirectory(strDir);
                }
                if (file != null)
                {
                    Stream sr = file.InputStream;
                    Bitmap bitmap = (Bitmap)Bitmap.FromStream(sr);
                    bitmap.Save(strPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 解析base64编码获取图片
        /// </summary>
        /// <param name="base64Code"></param>
        /// <returns></returns>
        private Bitmap Base64ToImg(string base64Code)
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64Code));
            return new Bitmap(stream);
        }

        /// <summary>
        /// 获取当前时间段额时间戳
        /// </summary>
        /// <returns></returns>
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
    }
}
