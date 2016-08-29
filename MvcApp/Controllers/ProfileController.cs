using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BLL.DTO;
using Microsoft.AspNet.Identity;
using Core.BLL.Interfaces;
using Core.POCO;
using POCO = Core.POCO;
using MvcApp.ViewModel;
using System.Threading.Tasks;
using MvcApp.Services;

namespace MvcApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        IUserService userService;
        IMappingService mappingService;
        IProfileService profileService;
        ICountriesService countriesService;
        ISessionService sessionService;
        public string CurrentUserId
        {
            get
            {
                return sessionService.CurrentUserId;
            }
        }

        public ProfileController(IUserService userService, IMappingService mappingService, IProfileService profileService, ICountriesService countriesService, ISessionService sessionService)
        {
            this.userService = userService;
            this.mappingService = mappingService;
            this.profileService = profileService;
            this.countriesService = countriesService;
            this.sessionService = sessionService;
        }

        [HttpGet]
        public ActionResult ProfilePage()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult MainInfo()
        {
            var profile = profileService.GetMainInfo(CurrentUserId);

            var mainInfoVM = mappingService.Map<Profile, MainViewModel>(profile);

            mainInfoVM.Languages = profileService.GetLanguages(CurrentUserId).Select(a => mappingService.Map<POCO.SelectListItem, System.Web.Mvc.SelectListItem>(a)).ToList();
            mainInfoVM.GenderList = profileService.GetGender(CurrentUserId).Select(a => mappingService.Map<POCO.SelectListItem, System.Web.Mvc.SelectListItem>(a)).ToList();
            mainInfoVM.MaritalStatuses = profileService.GetMaritalStatus(CurrentUserId).Select(a => mappingService.Map<POCO.SelectListItem, System.Web.Mvc.SelectListItem>(a)).ToList();
            mainInfoVM.BirthDays = profileService.GetBirthDays().Select(a => mappingService.Map<string, System.Web.Mvc.SelectListItem>(a)).ToList();
            mainInfoVM.BirthMonths = profileService.GetBirthMonths().Select(a => mappingService.Map<string, System.Web.Mvc.SelectListItem>(a)).ToList();
            mainInfoVM.BirthYears = profileService.GetBirthYears(CurrentUserId).Select(a => mappingService.Map<string, System.Web.Mvc.SelectListItem>(a)).ToList();

            return PartialView("MainInfo", mainInfoVM);
        }

        [HttpPost]
        public ActionResult MainInfo(MainViewModel vm)
        {
            var profile = mappingService.Map<MainViewModel, Profile>(vm);
            profileService.SaveMainInfo(profile, CurrentUserId);

            return new EmptyResult();
        }
        [HttpGet]
        public PartialViewResult ContactInfo()
        {
            var profile = profileService.GetContacts(CurrentUserId);

            var contactsVM = mappingService.Map<Profile, ContactsViewModel>(profile);
            contactsVM.PhoneNumber = userService.GetPhoneNumber(CurrentUserId);

            return PartialView("ContactInfo", contactsVM);
        }

        [HttpPost]
        public ActionResult ContactInfo(ContactsViewModel contactsVM)
        {
            if (ModelState.IsValid)
            {
                var profile = mappingService.Map<ContactsViewModel, Profile>(contactsVM);

                profileService.SaveContacts(profile);

                userService.UpdatePhoneNumber(CurrentUserId, contactsVM.PhoneNumber);

                ModelState.Clear();

                var refreshedContactInfo = new ContactsViewModel { Country = "", PhoneNumber = "", ProfileID = contactsVM.ProfileID, Skype = "", Town = "", WebSite = "" };

                return PartialView("ContactInfo", refreshedContactInfo);
            }
            return new EmptyResult();

        }


        [HttpGet]
        public ActionResult InterestsInfo()
        {
            var profile = profileService.GetInterests(CurrentUserId);

            var interestsVM = mappingService.Map<Profile, InterestsViewModel>(profile);

            return PartialView("InterestsInfo", interestsVM);
        }

        [HttpPost]
        public ActionResult InterestsInfo(InterestsViewModel interestsVM)
        {
            var profile = mappingService.Map<InterestsViewModel, Profile>(interestsVM);
            profileService.SaveInterests(profile, CurrentUserId);

            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult EducationInfo()
        {
            EducationDTO dto = profileService.GetEducationInfoOfUser(CurrentUserId);
            EducationViewModel model = mappingService.Map<EducationDTO, EducationViewModel>(dto);

            model.SchoolCountries = dto.CountriesList.Select(a => mappingService.Map<POCO.SelectListItem, System.Web.Mvc.SelectListItem>(a)).ToList();
            model.SchoolTowns = dto.Towns.Select(a => mappingService.Map<string, System.Web.Mvc.SelectListItem>(a.TownName)).ToList();
            model.StartYears = dto.StartYears.Select(a => mappingService.Map<string, System.Web.Mvc.SelectListItem>(a)).ToList();
            model.FinishYears = dto.FinishYears.Select(a => mappingService.Map<string, System.Web.Mvc.SelectListItem>(a)).ToList();

            return PartialView("EducationInfo", model);
        }

        [HttpPost]
        public ActionResult EducationInfo(EducationViewModel model)
        {
            var profile = mappingService.Map<EducationViewModel, Profile>(model);
            profileService.SaveEducation(profile, CurrentUserId);

            return new EmptyResult();
        }
        [HttpGet]
        public JsonResult GetFinishYears(int selectedStartYear)
        {
            string[] finishYears = profileService.GetFinishYears(selectedStartYear, CurrentUserId);

            return Json(finishYears, JsonRequestBehavior.AllowGet);
        }



    }
}
