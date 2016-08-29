using Ninject;
using Core.BLL.Interfaces;
using System;
using System.Web.ModelBinding;

namespace WebFormsApp
{
    public partial class EmailVerification : BasePage
    {
        [Inject]
        public IUserService UserService { get; set; }

        public void Page_InitComplete(object sender, EventArgs e)
        {
            lblMessage.Text = VerificationResult(Request.RequestContext.RouteData.Values["username"] as string);
        }

        public string VerificationResult(string username = null)
        {
            if (username == null)
                username = Request.QueryString["username"] as string;

            if (username != null)
            {
                var user = UserService.GetUserByName(username);

                string message;

                if (user != null)
                {
                    user.EmailConfirmed = true;
                    UserService.UpdateUser(user);

                    message = @"Ваш електронный адресс подтвержден. Перейдите по ссылке ниже:
                            <br/><a href='Login.aspx' style='text-decoration:underline;color:blue;'>Войти в свой аккаунт</a>";
                }
                else
                    message = "Возникла неизвестная ошибка.";

                return message;
            }
            return "";
        }


    }
}