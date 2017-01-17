using Core.BLL.Interfaces;
using Core.POCO;
using MvcApp.Services;
using MvcApp.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        IUserService userService;
        IProfileService profileService;
        ISessionService sessionService;
        ISettingsService settingsService;
        public SettingsController(IUserService userService, IProfileService profileService, ISessionService sessionService, ISettingsService settingsService)
        {
            this.userService = userService;
            this.profileService = profileService;
            this.sessionService = sessionService;
            this.settingsService = settingsService;
        }
        public string CurrentUserId
        {
            get
            {
                return sessionService.CurrentUserId;
            }
        }

        [HttpGet]
        public ActionResult UpdatePrivacyFlag(int PrivacyFlagTypeID, int ProfileID, int VisibilityLevelID)
        {
            try
            {
                var pf = new PrivacyFlag
                {
                    PrivacyFlagTypeID = PrivacyFlagTypeID,
                    ProfileID = ProfileID,
                    VisibilityLevelID = VisibilityLevelID
                };
                settingsService.UpdatePrivacyFlagForChoosenSection(pf);
            }
            catch
            {
                Response.StatusCode = 400;
            }
            return new EmptyResult();
        }
        [HttpGet]
        public ActionResult SettingsPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PasswordInfo()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult PasswordInfo(PasswordViewModel passwordVM)
        {
            try
            {
                string password = userService.GetPasswordByUserId(CurrentUserId);

                if (passwordVM.OldPassword == password)
                {
                    if (ModelState.IsValid)
                    {
                        userService.UpdatePassword(passwordVM.NewPassword, CurrentUserId);
                        ModelState.Clear();
                        var refreshedPasswordViewModel = new PasswordViewModel();

                        return PartialView(refreshedPasswordViewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong old password");
                }
            }
            catch
            {
                ModelState.AddModelError("", "There are some problems");
            }

            Response.StatusCode = 400;
            return PartialView(passwordVM);
        }


        [HttpGet]
        public ActionResult GetEmailInfo()
        {
            string oldEmail = profileService.GetEmail(CurrentUserId);

            var email = new EmailViewModel { OldEmail = oldEmail };

            return PartialView("EmailInfo", email);
        }

        [HttpPost]
        public ActionResult EmailInfo(EmailViewModel model)
        {
            if (ModelState.IsValid && model.OldEmail != model.NewEmail && !string.IsNullOrEmpty(model.NewEmail))
            {
                profileService.UpdateEmail(CurrentUserId, model.NewEmail);

                var refreshedEmailViewModel = new EmailViewModel();

                refreshedEmailViewModel.OldEmail = model.NewEmail;

                ModelState.Clear();

                return PartialView("EmailInfo", refreshedEmailViewModel);
            }
            else
            {
                Response.StatusCode = 400; //avoid calling onSuccess js method

                return PartialView("EmailInfo", model);
            }
        }

        [HttpGet]
        public ActionResult PhoneNumberInfo()
        {
            string phoneNumber = userService.GetPhoneNumber(CurrentUserId);

            var vm = new PhoneNumberViewModel { PhoneNumber = phoneNumber };

            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult PhoneNumberInfo(PhoneNumberViewModel vm)
        {
            if (ModelState.IsValid && vm.PhoneNumber != vm.NewPhoneNumber && !string.IsNullOrEmpty(vm.NewPhoneNumber))
            {
                userService.UpdatePhoneNumber(CurrentUserId, vm.NewPhoneNumber);

                var refreshedPhoneNumber = new PhoneNumberViewModel();

                refreshedPhoneNumber.PhoneNumber = vm.NewPhoneNumber;

                ModelState.Clear();

                return PartialView(refreshedPhoneNumber);
            }
            else
            {
                Response.StatusCode = 400;

                return PartialView(vm);
            }

        }
    }
}