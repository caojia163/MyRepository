using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace GAManage.Common
{
    public class LogHelper
    {
        public enum logLevel
        {
            DEBUG,
            INFO,
            WARN,
            ERROR,
            FATAL
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex,logLevel ll= logLevel.DEBUG)

        public static void WriteLog(Type t, Exception ex, logLevel ll = logLevel.DEBUG)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            switch (ll)
            {
                case logLevel.DEBUG:
                    log.Debug("Error", ex);
                    break;
                case logLevel.INFO:
                    log.Info("Error", ex);
                    break;
                case logLevel.WARN:
                    log.Warn("Error", ex);
                    break;
                case logLevel.ERROR:
                    log.Error("Error", ex);
                    break;
                case logLevel.FATAL:
                    log.Fatal("Error", ex);
                    break;
                default:
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, string msg,logLevel ll= logLevel.DEBUG)

        public static void WriteLog(Type t, string msg, logLevel ll = logLevel.DEBUG)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            switch (ll)
            {
                case logLevel.DEBUG:
                    log.Debug(msg);
                    break;
                case logLevel.INFO:
                    log.Info(msg);
                    break;
                case logLevel.WARN:
                    log.Warn(msg);
                    break;
                case logLevel.ERROR:
                    log.Error(msg);
                    break;
                case logLevel.FATAL:
                    log.Fatal(msg);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
