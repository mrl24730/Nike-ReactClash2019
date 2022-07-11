using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike_ReactClash2019.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string path = Server.MapPath("~/index.html");
            return File(path, "text/html");
        }
    }
}
