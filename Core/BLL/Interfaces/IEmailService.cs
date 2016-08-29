namespace  Core.BLL.Interfaces
{
    public interface IEmailService
    {
        string SendEmail(string To, string CC, string BCC, string Subject, string Message);
        string SendEmailAddressVerificationEmail(string Username, string To, string RootURL);
        void SendPasswordReminderEmail(string To, string EncryptedPassword, string Username);
    }
}