using Core.BLL.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp
{
    public partial class RecoverPasswordPage : System.Web.UI.Page
    {

        [Inject]
        public IEmailService EmailService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }



        void Page_Load(object s, EventArgs e)
        {
            if (IsPostBack)
                lblErrors.Text = RecoverPassword(txtEmail.Text);
        }

        public string RecoverPassword(string Email)
        {
            var user = UserService.GetUserByEmail(Email);

            if (user != null)

                try
                {
                    EmailService.SendPasswordReminderEmail(user.Email, user.Password, user.UserName);

                    return "An email was sent to your account!";
                }

                catch (Exception e)
                {
                    return e.Message;
                }

            else
                return "We couldn't find the account you requested.";

        }
    }
}