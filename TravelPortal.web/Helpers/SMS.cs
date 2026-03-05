using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TravelPortal.web.Helpers
{
    public class SMS
    {
        public static bool OtpEmail(string email, string userName)
        {
            var otp = new System.Random().Next(100000, 999999).ToString();
            string template = EmailService.LoadTemplate("otp");

            template = EmailService.ReplaceTokens(template, new
            {
                UserName = userName,
                OTP = otp
            });

            EmailService.SendEmail(email, "Registration OTP Code", template);
            SessionHelper.OTP = otp;
            // Return success (adjust as needed)
            return true;
        }
        public static bool ForgotPasswordEmail(string email, string userName)
        {
            var otp = new System.Random().Next(100000, 999999).ToString();
            string template = EmailService.LoadTemplate("ForgotPassword");

            template = EmailService.ReplaceTokens(template, new
            {
                UserName = userName,
                OTP = otp,
                Year = DateTime.Now.Year.ToString()
            });

            EmailService.SendEmail(email, "Forgot Password Verification Code", template);
            SessionHelper.OTP = otp;
            // Return success (adjust as needed)
            return true;
        }
        public static async Task<bool> WelcomeEmail(string email, string userName)
        {
            string template = EmailService.LoadTemplate("welcome");

            template = EmailService.ReplaceTokens(template, new
            {
                UserName = userName,
                AdditionalInfo = ""
            });

            EmailService.SendEmail(email, "Registration Welcome", template);

            // Return success (adjust as needed)
            return true;
        }
        public static async Task<bool> InvitationEmail(string email, string userName)
        {
            string template = EmailService.LoadTemplate("invitation");

            template = EmailService.ReplaceTokens(template, new
            {
                UserName = userName,
                InviterName = "",
                InvitationLink = ""
            });

            EmailService.SendEmail(email, "Registration Welcome", template);

            // Return success (adjust as needed)
            return true;
        }
        public static async Task<bool> KycCompletedEmail(string email, string userName)
        {
            string template = EmailService.LoadTemplate("invitation");

            template = EmailService.ReplaceTokens(template, new
            {
                UserName = userName,
                KycReference = ""
            });

            EmailService.SendEmail(email, "Registration Welcome", template);

            // Return success (adjust as needed)
            return true;
        }
        public static async Task<bool> ActivationAccount(string email, string userName, string code, string userid)
        {
            string template = EmailService.LoadTemplate("ActivationAccount");

            template = EmailService.ReplaceTokens(template, new
            {
                UserName = userName,
                verification_code = code,
                activation_link = $"{ConfigHelper.ApplicationUrl}/Account/AccountActivate?code={userid}"
            });

            EmailService.SendEmail(email, "Activate Your Account and Set Password", template);

            // Return success (adjust as needed)
            return true;
        }
    }
}