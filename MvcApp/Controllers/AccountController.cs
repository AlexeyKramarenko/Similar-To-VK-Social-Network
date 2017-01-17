using Core.BLL.DTO;
using Core.BLL.Interfaces;
using Core.POCO;
using MvcApp.Services;
using MvcApp.ViewModel;
using System;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    [RoutePrefix("account")]
    [Authorize]
    public class AccountController : Controller
    {
        public IUserService userService;
        public IEmailService emailService;
        public IMappingService mappingService;
       

        public AccountController(IUserService userService, IEmailService emailService, IMappingService mappingService)
        {
            this.userService = userService;
            this.emailService = emailService;
            this.mappingService = mappingService;
        }



        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                userService.LogoutUser();

            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        public string GetHost()
        {
            return Request.Url.Host;
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var loginObj = new LoginDTO { UserName = lvm.UserName, Password = lvm.Password };

                var result = userService.LoginUser(loginObj, GetHost);

                if (result.Succedeed)
                    return RedirectToAction("MainPage", "Main");

                else
                    ModelState.AddModelError("ErrorMessage", result.Message);
            }
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("recoverpassword")]
        public ActionResult RecoverPassword(string Email)
        {
            var user = userService.GetUserByEmail(Email);

            if (user != null)

                try
                {
                    emailService.SendPasswordReminderEmail(user.Email, user.Password, user.UserName);

                    ViewBag.Message = "An email was sent to your account!";
                }

                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                }

            else
                ViewBag.Message = "We couldn't find the account you requested.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("registration")]
        public ActionResult Registration()
        { 
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(RegistrationViewModel rvm)
        {
            //data from Web Api controller
            rvm.BirthDate = new DateTime(
                                int.Parse(Request.Form["BirthYear"].ToString()),
                                int.Parse(Request.Form["BirthMonth"].ToString()),
                                int.Parse(Request.Form["BirthDay"].ToString()));

            rvm.Country = Request.Form["Country"].ToString();
            rvm.Town = Request.Form["Town"].ToString();


            if (ModelState.IsValid)
            {
                var profile = new Profile
                {
                    Country = rvm.Country,
                    Town = rvm.Town,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    BirthDay = rvm.BirthDate.Day,
                    BirthMonth = rvm.BirthDate.Month,
                    BirthYear = rvm.BirthDate.Year,
                    Gender = rvm.Gender,
                    MaritalStatus = rvm.MaritalStatus,
                    CreationDate = DateTime.Now,

                    AboutMe = "",
                    Activity = "",
                    Books = "",
                    Games = "",
                    HomeTown = "",
                    Interests = "",
                    Language = "",
                    Music = "",
                    Quotes = "",
                    School = "",
                    SchoolCountry = "",
                    SchoolTown = "",
                    Skype = "",
                    StartSchoolYear = rvm.BirthDate.Year,
                    FinishSchoolYear = rvm.BirthDate.Year,
                    Films = "",
                    WebSite = "",
                    Wife = ""
                };

                var user = mappingService.Map<RegistrationViewModel, ApplicationUser>(rvm);

                OperationResult result = userService.CreateUser(profile, user, GetHost);

                if (result.Succedeed)
                    //return RedirectToAction("WaitingForConfirmation");
                    return RedirectToAction("Login");

                else
                    ModelState.AddModelError("error", result.Message);
            }
            return View();
        }

        [HttpGet]
        [Route("emailverification/{username}")]
        [AllowAnonymous]
        public ActionResult EmailVerification(string username)
        {
            //var userName = Request.RequestContext.RouteData.Values["username"] as string;
            //var userName = Request.QueryString["a"];

            if (username != null)
            {
                var user = userService.GetUserByName(username);

                if (user != null)
                {
                    user.EmailConfirmed = true;
                    userService.UpdateUser(user);

                    ViewBag.Message = @"Ваш електронный адресс подтвержден. Перейдите по ссылке ниже:
                            <br/><a href='/account/login' style='text-decoration:underline;color:blue;'>Войти в свой аккаунт</a>";
                }
                else
                    ViewBag.Message = "Пользователя с таким именем нет.";
            }
            else
                ViewBag.Message = "Не указано имя пользователя.";

            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult WaitingForConfirmation()
        {
            return View();
        }
    }
}
