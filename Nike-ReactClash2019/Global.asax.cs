using Kitchen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Nike_ReactClash2019
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception objErr = Server.GetLastError().GetBaseException();

            if (HttpContext.Current != null && HttpContext.Current.Request.IsLocal.Equals(false))
            {
                //Helper.ApplicationErrorHandler(sender, e, objErr);
            }
            string msg = objErr.Message.ToString() + Environment.NewLine + objErr.StackTrace.ToString() + Environment.NewLine;
            if (objErr.InnerException != null) msg += objErr.InnerException.ToString() + Environment.NewLine;

            msg += objErr.Source.ToString();

            AppHelper.WriteLog(msg);

            Response.Redirect("~/error.html");
            Server.ClearError();
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
