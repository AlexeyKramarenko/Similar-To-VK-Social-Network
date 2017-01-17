using Core.BLL.Interfaces;
using Core.POCO;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsApp.Services;
using WebFormsApp.ViewModel;

namespace WebFormsApp
{
    public partial class CreateUserPage : Page
    {
        private HttpRequestBase httpRequestBase;
        public HttpRequestBase RequestBase
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

            RequestBase = new HttpRequestWrapper(HttpContext.Current.Request);
        }
        [Inject]
        public IMappingService MappingService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IRedirectService Redirect { get; set; }

        public string GetHost()
        {
            return HttpContext.Current.Request.Url.Authority;
        }

        public void CreateUser(CreateUserViewModel rvm)
        {
            rvm.Country = RequestBase.Form["Country"];
            rvm.Town = RequestBase.Form["Town"];
            rvm.BirthDate = new DateTime(
                                int.Parse(RequestBase.Form["BirthYear"]),
                                int.Parse(RequestBase.Form["BirthMonth"]),
                                int.Parse(RequestBase.Form["BirthDay"]));


            if (ModelState.IsValid)
            {
                var profile = new Core.POCO.Profile
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
                    Language = "English",
                    AboutMe = "",
                    Activity = "",
                    Books = "",
                    Games = "",
                    HomeTown = "",
                    Interests = "",
                    Music = "",
                    Quotes = "",
                    School = "",
                    SchoolCountry = rvm.Country,
                    SchoolTown = rvm.Town,
                    Skype = "",
                    StartSchoolYear = rvm.BirthDate.Year + 7,
                    Films = "",
                    FinishSchoolYear = rvm.BirthDate.Year + 17,
                    WebSite = "",
                    Wife = ""
                };

                ApplicationUser user = MappingService.Map<CreateUserViewModel, ApplicationUser>(rvm);
                OperationResult result = UserService.CreateUser(profile, user, GetHost);

                if (result.Succedeed)
                    //Redirect.GoToWaitingForConfirmationPage();
                    Redirect.GoToLoginPage();
                else
                    ModelState.AddModelError("", result.Message);
            }
        }
    }
}