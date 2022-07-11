using Kitchen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Nike_ReactClash2019.Class
{
    public class Helper
    {
        public static void ApplicationErrorHandler(Object sender, EventArgs arg, Exception e)
        {
            string remoteIP = "Unknown";
            string requestUrl = "Unknown";
            var httpRequestBase = new HttpRequestWrapper(HttpContext.Current.Request);
            remoteIP = AppHelper.GetIPAddress(httpRequestBase);
            requestUrl = httpRequestBase.Url.ToString();

            string message = e.Message.ToString();
            string stacktrace = e.StackTrace.ToString();

            try
            {
                //error - notify by email
                string title = httpRequestBase.ServerVariables["HTTP_HOST"];
                //Emailer.sendMail(new string[] { "it@kitchen-digital.com" }, "it@kitchen-digital.com", title, msg, "App Error Log");
                Task.Run(async () => await HttpPostAsync(title, requestUrl, message, remoteIP));
            }
            catch { }

        }

        private static async Task<bool> HttpPostAsync(string title, string path, string message, string remoteIP)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new Dictionary<string, string>
                {
                    { "server", title },
                    { "path", path },
                    { "message", $"{{\"message\": \"{message}\", \"remoteIP\": \"{remoteIP}\"}}" }
                };
                var body = new FormUrlEncodedContent(content);
                var response = await client.PostAsync("https://secure.thekitchen.hk/tg_notification/sendMessage.php", body);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
