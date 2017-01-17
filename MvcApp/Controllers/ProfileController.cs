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

            mainInfoVM.MaritalStatuses = new List<SelectListItem> { new SelectListItem { Text = "Single", Value = "Single" }, new SelectListItem { Text = "Married", Value = "Married" } };
            mainInfoVM.Languages = new List<SelectListItem> { new SelectListItem { Text = "Ukrainian", Value = "Ukrainian" }, new SelectListItem { Text = "English", Value = "English" } };
            mainInfoVM.GenderList = new List<SelectListItem> { new SelectListItem { Text = "Male", Value = "Male" }, new SelectListItem { Text = "Female", Value = "Female" } };
            mainInfoVM.BirthDays = profileService.GetBirthDays().Select(a => mappingService.Map<string, SelectListItem>(a)).ToList();
            mainInfoVM.BirthMonths = profileService.GetBirthMonths().Select(a => mappingService.Map<string, SelectListItem>(a)).ToList();
            mainInfoVM.BirthYears = profileService.GetBirthYears(CurrentUserId).Select(a => mappingService.Map<string, SelectListItem>(a)).ToList();

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
            else
            {
                Response.StatusCode = 400;

                string message = "";

                for (int i = 0; i < ModelState.Values.Count; i++)
                {
                    ModelErrorCollection col = ModelState.Values.ElementAt(i).Errors;

                    foreach (var msg in col)
                        message += msg.ErrorMessage;
                }

                return Json(message, JsonRequestBehavior.AllowGet);
            }

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

        private static int graduationYear = 0;

        [HttpGet]
        public ActionResult EducationInfo()
        {
            EducationDTO dto = profileService.GetEducationInfoOfUser(CurrentUserId);
            EducationViewModel model = mappingService.Map<EducationDTO, EducationViewModel>(dto); 
            model.SchoolCountries = ListItemsFromStringArray(dto.CountriesList);
            model.SchoolTowns = ListItemsFromStringArray(dto.Towns.Select(a => a.TownName));
            model.StartYears = ListItemsFromStringArray(dto.StartYears);
            model.FinishYears = ListItemsFromStringArray(dto.FinishYears); 
            return PartialView("EducationInfo", model);
        }

        [HttpPost]
        public ActionResult EducationInfo(EducationViewModel model)
        {
            var profile = mappingService.Map<EducationViewModel, Profile>(model);
            profileService.SaveEducation(profile, CurrentUserId); 
            return Json(profile.FinishSchoolYear);
        }
        [HttpGet]
        public JsonResult GetFinishYears(int selectedStartYear)
        {
            string[] finishYears = profileService.GetFinishYears(selectedStartYear, CurrentUserId);

            return Json(finishYears, JsonRequestBehavior.AllowGet);
        }



        private SelectListItem[] ListItemsFromStringArray(IEnumerable<string> str)
        {
            var items = new SelectListItem[str.Count()];

            for (int i = 0; i < str.Count(); i++)
            {
                items[i] = new SelectListItem { Text = str.ElementAt(i), Value = str.ElementAt(i) };
            }

            return items;
        }
    }
}
