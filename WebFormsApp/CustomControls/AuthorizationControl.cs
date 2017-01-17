using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp.CustomControls
{
    [ToolboxData("<{0}:AuthorizationControl runat='server'></{0}:AuthorizationControl>")]
    public class AuthorizationControl : Control
    {

        public string CurrentPage
        {
            get
            {
                return (string)ViewState["CurrentPage"];
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        
        public string LoginPage
        {
            get
            {
                return (string)ViewState["LoginPage"];
            }
            set
            {
                ViewState["LoginPage"] = value;
            }
        }
        
        public string RegistrationPage
        {
            get
            {
                return (string)ViewState["RegistrationPage"];
            }
            set
            {
                ViewState["RegistrationPage"] = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        { 
            switch (CurrentPage.ToLower())
            {
                case "createuserpage.aspx":

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, LoginPage);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write("Login");
                    writer.RenderEndTag();

                    break;


                case "loginpage.aspx":

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, RegistrationPage);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write("Registration");
                    writer.RenderEndTag();

                    break;

                case "waitingforconfirmation.aspx":
  
                    break;

                default:

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, LoginPage);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write("Logout");
                    writer.RenderEndTag();
                    
                    break;
            }
        }
    }
}
