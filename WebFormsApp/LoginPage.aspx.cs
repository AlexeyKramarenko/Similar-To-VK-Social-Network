using Core.BLL.DTO;
using Core.BLL.Interfaces;
using Core.DAL;
using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsApp.Services;
using WebFormsApp.ViewModel;

namespace WebFormsApp
{
    public partial class LoginPage : System.Web.UI.Page
    {
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public ISessionService SessionService { get; set; }
        [Inject]
        public IRedirectService Redirect { get; set; }

        void Page_Load(object s, EventArgs e)
        {
            if (!IsPostBack && User.Identity.IsAuthenticated)
                UserService.LogoutUser();
        }
        public string GetHost()
        {
            return Request.Url.Host;
        }

        public void LoginUser(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var loginObj = new LoginDTO { UserName = lvm.UserName, Password = lvm.Password };

                var result = UserService.LoginUser(loginObj, GetHost);

                if (result.Succedeed)
                {
                    SessionService.CurrentUserId = result.Id;
                    Redirect.GoToMainPage();
                }
                else
                    ModelState.AddModelError("", result.Message);
            }
        }
    }
}