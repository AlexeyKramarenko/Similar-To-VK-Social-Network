using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using WebFormsApp.ViewModel;
using System;
using Microsoft.Owin;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using Core.POCO;
using Core.BLL.Interfaces;

namespace WebFormsApp
{
    public partial class Registration : BasePage
    {
        [Inject]
        public IMappingService MappingService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }


        public string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }


        void Page_Load(object s, EventArgs e)
        {
            if (IsPostBack)
                literalForCallJS.Text = "<script>getRegistrationDataFromStorage();</script>";
        }

        public void CreateUser(RegistrationViewModel rvm)
        {
            rvm.Country = Request.Form["BirthYear"];
            rvm.Town = Request.Form["Town"];
            rvm.BirthDate = new DateTime(
                                int.Parse(Request.Form["BirthYear"]),
                                int.Parse(Request.Form["BirthMonth"]),
                                int.Parse(Request.Form["BirthDay"]));


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
                    MaritalStatus = rvm.Married,
                    CreateDate = DateTime.Now,

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
                    StartSchoolYear = rvm.BirthDate.Year+7,
                    Films = "",
                    FinishSchoolYear = rvm.BirthDate.Year+17,
                    WebSite = "",
                    Wife = ""
                };

                var user = MappingService.Map<RegistrationViewModel, ApplicationUser>(rvm);

                var result = UserService.CreateUser(profile, user, GetHost);

                if (result.Succedeed)
                    this.Redirect("~/WaitingForConfirmation.aspx");

                else
                    ModelState.AddModelError("", result.Message);
            }
        }
    }
}
