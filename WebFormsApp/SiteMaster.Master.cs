
using WebFormsApp.CustomControls;
using System;
using System.Web;
using WebFormsApp;

namespace WebFormsApp
{
    public partial class SiteMaster : MasterPageBase
    {
 

        protected void Page_Load(object sender, EventArgs e)
        {
            string currentPage = Request.CurrentExecutionFilePath.Replace("/", "");

            hrefPlaceholder.Controls.Add( 
                        new AuthorizationControl
                        {
                            CurrentPage = currentPage,
                            RegistrationPage = "Registration.aspx",
                            LoginPage = "Login.aspx"                     
                        } 
                ); 
        }

       


    }
}