using Core.POCO;

namespace  Core.BLL.Interfaces
{
    public interface IEmailService
    {
        OperationResult SendEmail(string To, string CC, string BCC, string Subject, string Message);
        OperationResult SendEmailAddressVerificationEmail(string Username, string To, string RootURL);
        void SendPasswordReminderEmail(string To, string EncryptedPassword, string Username);
    }
}