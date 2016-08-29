using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {

        [HttpGet]       
        public ActionResult PeoplePage(string UserID, bool Online)
        {

            return View();
        }
    }
}