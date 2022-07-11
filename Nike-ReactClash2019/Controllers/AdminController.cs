using Newtonsoft.Json;
using Nike_ReactClash2019.Class;
using Nike_ReactClash2019.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Nike_ReactClash2019.Controllers
{
    public class AdminController : Controller
    {
        private static readonly string AdminPassword = (string)System.Configuration.ConfigurationManager.AppSettings["admin:password"];

        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string Filename = "CushionWorkshop2019-Records.xlsx";
        private const string WorksheetsTitle = "2019 Cushion Workshop Registration Records";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Download()
        {
            if (Session["haAdmin"] == null)
                return RedirectToAction("Index", "Admin");
            Session["haAdmin"] = null;

            byte[] buffer = ExportRecords();

            var contentDisposition = new ContentDisposition
            {
                FileName = Filename,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", contentDisposition.ToString());

            return File(buffer, ContentType);
        }

        private byte[] ExportRecords()
        {
            List<string> rowHeaders = new List<string> {
                "Name", "Phone", "Email", "Birthday",
                "Gender", "Timeslot ID", "Timeslot Name", "Timeslot Group",
                "Subscribe", "Registered At"
            };

            using (ExcelPackage package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add(WorksheetsTitle);
                int contentStartRow = 2;

                var reports = DBHelper.GetReports();

                ws.Cells[1, 1, 1, rowHeaders.Count].Style.Font.Bold = true;

                for (var i = 0; i < rowHeaders.Count; i++)
                {
                    ws.Cells[1, i + 1].Value = rowHeaders[i];
                }

                foreach (var report in reports)
                {
                    string gender;
                    if (report.record.IsMale == null)
                    {
                        gender = "N/A";
                    }
                    else if (report.record.IsMale == false)
                    {
                        gender = "F";
                    }
                    else
                    {
                        gender = "M";
                    }

                    ws.Cells[contentStartRow, 1].Value = report.record.Name;
                    ws.Cells[contentStartRow, 2].Value = report.record.Phone;
                    ws.Cells[contentStartRow, 3].Value = report.record.Email;
                    ws.Cells[contentStartRow, 4].Value = report.record.Birth == null ? "N/A" : report.record.Birth.Value.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    ws.Cells[contentStartRow, 5].Value = gender;
                    ws.Cells[contentStartRow, 6].Value = report.record.Tid;
                    ws.Cells[contentStartRow, 7].Value = report.timeslot.Name_tc;
                    ws.Cells[contentStartRow, 8].Value = report.timeslot.Slot_group;
                    //ws.Cells[contentStartRow, 9].Value = report.record.Lang == false ? "EN" : "TC";
                    ws.Cells[contentStartRow, 9].Value = report.record.Subscribe;
                    ws.Cells[contentStartRow, 10].Value = report.record.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    contentStartRow++;
                }
                return package.GetAsByteArray();
            }
        }

        [HttpPost]
        public ContentResult Login(FormCollection formData)
        {
            Response response = new Response();

            Session["haAdmin"] = null;
            if (!String.IsNullOrWhiteSpace(formData["username"]) && !String.IsNullOrWhiteSpace(formData["password"]))
            {
                string username = formData["username"];
                string password = formData["password"];

                //if (username == "nikeadmin" && password == "n1k3HyperAdapt19#")
                if (username == "admin" && password == AdminPassword)
                {
                    Session["haAdmin"] = "nike";
                    response.Code = ResponseCode.Success;
                }
                else
                {
                    response.Code = ResponseCode.Failed;
                }
            }
            else
            {
                response.Code = ResponseCode.Failed;
            }


            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

    }
}