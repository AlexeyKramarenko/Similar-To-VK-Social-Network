using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsApp.CustomControls;

namespace WebFormsApp
{
    public partial class Site : MasterPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentPage = Request.CurrentExecutionFilePath.Replace("/", "");

            hrefPlaceholder.Controls.Add(
                        new AuthorizationControl
                        {
                            CurrentPage = currentPage,
                            RegistrationPage = "CreateUserPage.aspx",
                            LoginPage = "LoginPage.aspx"
                        }
                );
        }




    }
}