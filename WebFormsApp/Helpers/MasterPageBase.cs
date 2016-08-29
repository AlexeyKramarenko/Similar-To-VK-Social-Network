using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebFormsApp
{
    public class MasterPageBase : Ninject.Web.MasterPageBase
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
       
            Request = new HttpRequestWrapper(HttpContext.Current.Request);
        }

    }
}
