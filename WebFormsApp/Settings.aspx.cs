using Core.BLL.Interfaces;
using Microsoft.AspNet.Identity;
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
    public partial class Settings : System.Web.UI.Page
    {
        [Inject]
        public IProfileService ProfileService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public ISessionService SessionService { get; set; }

        public string CurrentUserId
        {
            get
            {
                return SessionService.CurrentUserId;
            }
        }


        public EmailViewModel SelectEmail()
        {
            string oldEmail = ProfileService.GetEmail(CurrentUserId);
            return new EmailViewModel { OldEmail = oldEmail };
        }

        public async void UpdatePassword(PasswordViewModel vm)
        {
            try
            {
                string password = UserService.GetPasswordByUserId(CurrentUserId);

                if (vm.OldPassword == password)
                {
                    if (ModelState.IsValid)
                        await UserService.UpdatePassword(vm.NewPassword, CurrentUserId);
                }
                else
                    ModelState.AddModelError("", "Введен неверный пароль");
            }
            catch
            {
                ModelState.AddModelError("", "Возникли некоторые проблемы");
            }
        }



        public void UpdateEmail(EmailViewModel vm)
        {
            if (ModelState.IsValid && vm.OldEmail != vm.NewEmail)
                ProfileService.UpdateEmail(CurrentUserId, vm.NewEmail);
        }

        public PhoneNumberViewModel SelectPhoneNumber()
        {
            string userId = User.Identity.GetUserId();
            string phoneNumber = UserService.GetPhoneNumber(userId);

            var vm = new PhoneNumberViewModel { PhoneNumber = phoneNumber };
            return vm;
        }

        public void UpdatePhoneNumber(PhoneNumberViewModel vm)
        {
            if (ModelState.IsValid && vm.PhoneNumber != vm.NewPhoneNumber)
                UserService.UpdatePhoneNumber(CurrentUserId, vm.NewPhoneNumber);
        }
    }
}