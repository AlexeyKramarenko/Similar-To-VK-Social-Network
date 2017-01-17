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
    public partial class EmailVerification : WebFormsApp.Helpers.BasePage
    {
        [Inject]
        public IUserService UserService { get; set; }

        public void Page_InitComplete(object sender, EventArgs e)
        {
            string userName = Request.RequestContext.RouteData.Values["username"] as string;

            lblMessage.Text = VerificationResult(userName);
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