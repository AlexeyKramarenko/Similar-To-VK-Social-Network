using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Ninject;
using WebFormsApp.ViewModel;
using System;
using System.Web; 
using Core.BLL.DTO;
using Core.BLL.Interfaces;

namespace WebFormsApp
{
    public partial class Login : BasePage
    {
        [Inject]
        public IUserService UserService { get; set; }

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
                    this.Redirect("~/Main.aspx");
                else
                    ModelState.AddModelError("", result.Message);
            }
        }
    }
}