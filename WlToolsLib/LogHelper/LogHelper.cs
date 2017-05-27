using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace WlToolsLib.LogHelper
{
    #region --日志记录--
    public class LogHelper
    {
        private static readonly ILog _logError = LogManager.GetLogger("ErrorLogger");
        private static readonly ILog _logInfo = LogManager.GetLogger("InfoLogger");
        //private static bool isConfig = false;
        public LogHelper()
        {
            //if (!isConfig)
            //{
            //    XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("/") + "log4net.config"));
            //    isConfig = true;
            //}
        }
        public static void LogError(Exception error)
        {
            new Task(() => {
                try
                {
                    _logError.Error(error.Message);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }).Start();
        }

        public static void LogError(string infoStr)
        {
            new Task(() => {
                try
                {
                    _logError.Error(infoStr);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }).Start();
        }

        public static void LogInfo(string infoStr)
        {
            new Task(() => {
                try
                {
                    _logInfo.Info(infoStr);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }).Start();
        }
    }
    #endregion
}
