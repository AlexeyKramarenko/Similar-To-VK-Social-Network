using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFormsApp.Services
{
    public interface IRedirectService
    {
        void GoToRegistrationPage();
        void GoToLoginPage();
        void GoToEmailVerificationPage();
        void GoToMessagesPage();
        void GoToMainPage();
        void GoToPhotosPage();
        void GoToSettingsPage();
        void GoToProfilePage();
        void GoToRecoverPasswordPage();
        void GoToWaitingForConfirmationPage();
    }
}
