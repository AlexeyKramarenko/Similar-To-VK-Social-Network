
using  Core.BLL.Interfaces;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace  Core.BLL
{
    public class EmailService : IEmailService
    {
        string FROM_EMAIL_ADDRESS;
        string FROM_EMAIL_ADDRESS_PASSWORD;
        string EMAIL_HOST;
        string EMAIL_PORT;

        public EmailService()
        {
            FROM_EMAIL_ADDRESS = ConfigurationManager.AppSettings["FROM_EMAIL_ADDRESS"];
            FROM_EMAIL_ADDRESS_PASSWORD = ConfigurationManager.AppSettings["FROM_EMAIL_ADDRESS_PASSWORD"];
            EMAIL_HOST = ConfigurationManager.AppSettings["EMAIL_HOST"];
            EMAIL_PORT = ConfigurationManager.AppSettings["EMAIL_PORT"];
        }

        public void SendPasswordReminderEmail(string To, string EncryptedPassword, string Username)
        {
            string Message = "Here is the password you requested: " + EncryptedPassword;

            SendEmail(To, "", "", "Password Reminder", Message);
        }

        public string SendEmailAddressVerificationEmail(string Username, string To, string RootURL)
        {
            string msg = "Please click on the link below.<br/><br/>" +
                         "<a href=\"" + RootURL + "/EmailVerification/" + Username + "\"   > EmailVerification </a>";

            return SendEmail(To, "", "", "Account created! Email verification required.", msg);
        }


        public string SendEmail(string To, string CC, string BCC, string Subject, string Message)
        {
            try
            {
                var smtp_client = new SmtpClient(EMAIL_HOST, Convert.ToInt32(EMAIL_PORT));
                smtp_client.UseDefaultCredentials = false;
                smtp_client.Credentials = new NetworkCredential(FROM_EMAIL_ADDRESS, FROM_EMAIL_ADDRESS_PASSWORD);
                smtp_client.EnableSsl = true;


                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(FROM_EMAIL_ADDRESS);

                if (To != null)
                    msg.To.Add(new MailAddress(To));

                if (!string.IsNullOrEmpty(CC))
                    msg.CC.Add(new MailAddress(CC));

                if (!string.IsNullOrEmpty(BCC))
                    msg.Bcc.Add(new MailAddress(BCC));

                msg.Subject = Subject;
                msg.Body = Message;
                msg.IsBodyHtml = true;

                smtp_client.Send(msg);

            }
            catch (Exception e)
            {
                return e.Message;
            }

            return string.Empty;
        }


    }


}
