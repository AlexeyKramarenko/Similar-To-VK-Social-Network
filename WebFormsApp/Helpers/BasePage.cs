
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp.Helpers
{
    public class BasePage : Page
    {
        private HttpRequestBase httpRequestBase;
        public new HttpRequestBase Request
        {
            get
            {
                return httpRequestBase;
            }
            set
            {
                httpRequestBase = value;
            }
        }
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
      
            Request = new HttpRequestWrapper(HttpContext.Current.Request);
        }


        public void AddMessageToValidationSummaryCtrl(string errorMessage)
        {
            var err = new CustomValidator();
            err.IsValid = false;
            err.ErrorMessage = errorMessage;
            Page.Validators.Add(err);
        }
    }
}
