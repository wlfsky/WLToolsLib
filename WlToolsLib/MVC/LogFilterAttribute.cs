// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    LogFilterAttribute
// CreateTime:  2017/07/12 18:18:33
// Creator:     weilai
// FileRemark:  
// ------------------------------------


namespace WlToolsLib.MVC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using WlToolsLib.Expand;
    using WlToolsLib.JsonHelper;
    using WlToolsLib.LogHelper;


    /// <summary>
    /// 常规日志特性，
    /// </summary>
    public class LogFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.NotNull())
            {
                var controller = filterContext.RouteData.Values["controller"].NotNull() ? filterContext.RouteData.Values["controller"] : "";
                var action = filterContext.RouteData.Values["action"].NotNull() ? filterContext.RouteData.Values["action"] : "";
                var url = string.Format("{0}/{1}", controller, action);

                if (filterContext.Exception.NotNull())
                {
                    url.ErrLog(filterContext.Exception);
                }

                Dictionary<string, string> input = new Dictionary<string, string>();
#if DEBUG
                var currQS = filterContext.RequestContext.HttpContext.Request.Params;
                foreach (var item in currQS)
                {
                    var n = item.ToString();
                    input.Add(n, currQS[n]);
                }
                url.DebugLog(input.ToJson());
#else
                var currQS = filterContext.RequestContext.HttpContext.Request.QueryString;
                var currQS1 = filterContext.RequestContext.HttpContext.Request.Form;
                foreach (var item in currQS)
                {
                    var n = item.ToString();
                    input.Add(n, currQS[n]);
                }
                foreach (var item in currQS1)
                {
                    var n = item.ToString();
                    input.Add(n, currQS[n]);
                }
                url.InfoLog(input.ToJson());
#endif
            }
        }
    }
}
