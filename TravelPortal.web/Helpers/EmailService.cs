using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace TravelPortal.web.Helpers
{
    public static class EmailService
    {
        static private readonly string FromEmail = ConfigurationManager.AppSettings["FromEmail"];
        static private readonly string Password = ConfigurationManager.AppSettings["Password"];
        static private readonly string SMTP = ConfigurationManager.AppSettings["SMTP"];
        static private readonly int Port = int.Parse(ConfigurationManager.AppSettings["Port"]);

        public static bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(SMTP, Port);
                smtp.Credentials = new NetworkCredential(FromEmail, Password);
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string LoadTemplate(string templateName)
        {
            string path = HttpContext.Current.Server.MapPath("~/EmailTemplates/" + templateName + ".html");
            return File.ReadAllText(path);
        }

        public static string ReplaceTokens(string template, object model)
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                template = template.Replace("{{" + prop.Name + "}}", prop.GetValue(model)?.ToString());
            }
            return template;
        }
    }
}