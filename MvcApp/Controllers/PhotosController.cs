using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        [HttpGet]
        public ActionResult PhotosPage()
        {
            return View();
        }
    }
}