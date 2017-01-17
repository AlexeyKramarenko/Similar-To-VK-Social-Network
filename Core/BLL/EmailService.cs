
using Core.BLL.Interfaces;
using Core.POCO;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Core.BLL
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

        public OperationResult SendEmailAddressVerificationEmail(string Username, string To, string RootURL)
        {
            string msg = "Please click on the link below.<br/><br/>" +
                         "<a href=\"" + RootURL + "/EmailVerification/" + Username + "\"   > EmailVerification </a>";

            return SendEmail(To, "", "", "Account created! Email verification required.", msg);
        }

        public OperationResult SendEmail(string To, string CC, string BCC, string Subject, string Message)
        {
            try
            {
                var msg = new MailMessage
                {
                    From = new MailAddress(FROM_EMAIL_ADDRESS),
                    Subject = Subject,
                    IsBodyHtml = true,
                    Body = Message
                };

                if (!string.IsNullOrEmpty(To))
                    msg.To.Add(new MailAddress(To));

                if (!string.IsNullOrEmpty(CC))
                    msg.CC.Add(new MailAddress(CC));

                if (!string.IsNullOrEmpty(BCC))
                    msg.Bcc.Add(new MailAddress(BCC));


                var smtpClient = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential(FROM_EMAIL_ADDRESS, FROM_EMAIL_ADDRESS_PASSWORD)
                };

                smtpClient.Send(msg);
            }
            catch (Exception e)
            {
                return new OperationResult { Message = e.Message, Succedeed = false };
            }

            return new OperationResult { Succedeed = true };
        }

        public void SendPasswordReminderEmail(string To, string EncryptedPassword, string Username)
        {
            string Message = "Here is the password you requested: " + EncryptedPassword;

            SendEmail(To, "", "", "Password Reminder", Message);
        }
    }


}
