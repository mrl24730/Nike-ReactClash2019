using Kitchen;
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
    public class RecordController : ApiController
    {
        public IHttpActionResult Post([FromBody]Record record)
        {
            Response response = new Response();
            if (record == null || !ModelState.IsValid || record.Birth >= (DateTime.Now.AddDays(-1)))
            {
                response.Code = ResponseCode.Failed;
                response.Message = String.Join(
                    Environment.NewLine,
                    ModelState.Values
                        .SelectMany(val => val.Errors)
                        .Select(val => val.ErrorMessage)
                );

                return Json(response);
            }

            record.EncryptedPhone = CryptoHelper.encryptAES(record.Phone.ToString(), DBHelper.defaultSKey, DBHelper.fixedIV);
            record.EncryptedEmail = CryptoHelper.encryptAES(record.Email.ToLower(), DBHelper.defaultSKey, DBHelper.fixedIV);

            bool isPhoneExist = DBHelper.IsExistByPhone(record.EncryptedPhone);
            if (isPhoneExist)
            {
                response.Code = ResponseCode.AlreadyRegistered;
                return Json(response);
            }

            int status = DBHelper.AddRecord(record);
            if (status == 1)
            {
                response.Code = ResponseCode.Failed;
            }
            else if (status == 2)
            {
                response.Code = ResponseCode.Soldout;
            }
            return Json(response);
        }
    }
}
