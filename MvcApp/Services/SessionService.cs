
using System.Web;
using Microsoft.AspNet.Identity;

namespace MvcApp.Services
{
    public class SessionService : ISessionService
    {
        public string CurrentUserId
        {
            get
            {
                if (HttpContext.Current.Session["CurrentUserId"] == null)
                    HttpContext.Current.Session["CurrentUserId"] = HttpContext.Current.User.Identity.GetUserId();

                return HttpContext.Current.Session["CurrentUserId"].ToString();
            }
        }
    }
}
