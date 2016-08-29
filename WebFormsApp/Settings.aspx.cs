using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using WebFormsApp.ViewModel;
using System; 
using Core.BLL.Interfaces;

namespace WebFormsApp
{
    public partial class Settings : BasePage
    {
        private string currentUserId;
        
        [Inject]
        public IProfileService ProfileService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        void Page_Init(object s, EventArgs e)
        {
            currentUserId = User.Identity.GetUserId();
        }

        public EmailViewModel SelectEmail()
        {
            string oldEmail = ProfileService.GetEmail(currentUserId);
            return new EmailViewModel { OldEmail = oldEmail };
        }

        public async void UpdatePassword(PasswordViewModel vm)
        {
            try
            {
                string password = UserService.GetPasswordByUserId(currentUserId);

                if (vm.OldPassword == password)
                {
                    if (ModelState.IsValid)
                        await UserService.UpdatePassword(vm.NewPassword, currentUserId);
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
                ProfileService.UpdateEmail(currentUserId, vm.NewEmail);

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
                UserService.UpdatePhoneNumber(currentUserId, vm.NewPhoneNumber);

        }

    }
}