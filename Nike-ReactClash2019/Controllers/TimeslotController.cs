using Nike_ReactClash2019.Class;
using Nike_ReactClash2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nike_ReactClash2019.Controllers
{
    public class TimeslotController : ApiController
    {

        public IHttpActionResult Get()
        {
            Response response = new Response();

            try
            {
                response.Data = DBHelper.GetTimeslot();
            }
            catch (Exception e)
            {
                response.Code = ResponseCode.Failed;
            }

            return Json(response);
        }
    }
}
