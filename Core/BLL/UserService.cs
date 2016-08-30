using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Core.BLL.DTO;
using Core.BLL.Interfaces;
using Core.DAL;
using Core.DAL.Interfaces;
using Core.POCO;
using System;
using System.Web;

namespace Core.BLL
{
    public class UserService : IUserService
    {
        IUnitOfWork Database;
        IEmailService emailService;
        IProfileService profileService;
        IPhotoService photoService;
        ISettingsService settingsService;
        public UserService(IUnitOfWork db, IEmailService _emailService, IProfileService _profileService, IPhotoService _photoService, ISettingsService _settingsService)
        {
            Database = db;
            emailService = _emailService;
            profileService = _profileService;
            photoService = _photoService;
            settingsService = _settingsService;
        }
        public void LogoutUser()
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }

        public OperationResult LoginUser(LoginDTO loginObj, Func<string> host)
        {
            var userStore = new UserStore<ApplicationUser>(new DBContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            //Debug
            ApplicationUser user = new ApplicationUserManager(userStore, new DBContext()).GetUserByName(loginObj.UserName);

            //Release
            //ApplicationUser user = userManager.Find(loginObj.UserName, loginObj.Password);

            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, userIdentity);

                    return new OperationResult(succedeed: true);

                }
                else
                {
                    emailService.SendEmailAddressVerificationEmail(user.UserName, user.Email, host.Invoke());

                    return new OperationResult(succedeed: false,
                                               message: @"The login information you provided was correct 
                                                          but your email address has not yet been verified.  
                                                          We just sent another email verification email to you.  
                                                          Please follow the instructions in that email.");
                }
            }
            else
                return new OperationResult(succedeed: false, message: "We were unable to log you in with that information!");

        }

        public OperationResult CreateUser(Profile profile, ApplicationUser user, Func<string> host)
        {
            if (EmailAlreadyExists(user.Email))
                return new OperationResult(succedeed: false, message: "This email is already in use! ");

            else if (UsernameAlreadyExists(user.UserName))
                return new OperationResult(succedeed: false, message: "This username is already in use! ");

            else
            {
                /* 
                var store = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(store);
                var user = new IdentityUser() { UserName = vm.UserName };
                IdentityResult result = manager.Create(user, rvm.Password);
                */

                var store = new UserStore<ApplicationUser>(new DBContext());
                var manager = new UserManager<ApplicationUser>(store);

                user.CreateDate = DateTime.Now;

                string sendingResult = emailService.SendEmailAddressVerificationEmail(user.UserName, user.Email, host());

                if (sendingResult == string.Empty)
                {
                    IdentityResult result = manager.Create(user, user.Password);

                    if (result.Succeeded)
                    {
                        string userId = user.Id;

                        int profileId = profileService.AttachProfileToUser(userId, profile);
                        settingsService.AttachPrivacyToUserProfile(profileId);
                        photoService.AttachMainWallPhotoAlbumToUser(userId);
                        photoService.CreateDefaultAvatar(userId);

                        manager.AddToRole(userId, "public");
                        manager.AddToRole(userId, "registered");

                        return new OperationResult(succedeed: true);
                    }
                    else
                    {
                        string errors = "";

                        foreach (string e in result.Errors)
                            errors += e;

                        return new OperationResult(succedeed: false, message: errors);
                    }
                }
                else
                    return new OperationResult(succedeed: false, message: sendingResult);
            }
        }

        public async System.Threading.Tasks.Task UpdatePassword(string password, string currentUserId)
        {
            var store = new UserStore<ApplicationUser>(new DBContext());
            var UserManager = new UserManager<ApplicationUser>(store);
            var hashedNewPassword = UserManager.PasswordHasher.HashPassword(password);
            ApplicationUser user = await store.FindByIdAsync(currentUserId);

            await store.SetPasswordHashAsync(user, hashedNewPassword);
            await store.UpdateAsync(user);
        }

        public bool UsernameAlreadyExists(string Username)
        {
            ApplicationUser user = Database.UserManager.GetUserByName(Username);
            if (user != null)
                return true;

            return false;
        }

        public bool EmailAlreadyExists(string Email)
        {
            ApplicationUser user = Database.UserManager.GetUserByEmail(Email);

            if (user != null)
                return true;

            return false;
        }


        public ApplicationUser GetUserByName(string username)
        {
            var user = Database.UserManager.GetUserByName(username);
            return user;
        }

        public void UpdateUser(ApplicationUser user)
        {
            Database.UserManager.UpdateUser(user);
            Database.Save();
        }

        public void UpdatePhoneNumber(string currentUserId, string phoneNumber)
        {
            Database.UserManager.UpdatePhoneNumber(currentUserId, phoneNumber);
            Database.Save();
        }

        public string GetPhoneNumber(string userId)
        {
            string phoneNumber = Database.UserManager.GetPhoneNumber(userId);
            return phoneNumber;
        }

        public string GetUsernameByID(string interlocutorUserID)
        {
            return Database.UserManager.GetUserNameByUserID(interlocutorUserID);
        }

        public ApplicationUser GetAccount(string currentUserId)
        {
            ApplicationUser user = Database.UserManager.GetAccount(currentUserId);
            return user;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            ApplicationUser user = Database.UserManager.GetUserByEmail(email);
            return user;
        }

        public string GetPasswordByUserId(string currentUserId)
        {
            string pswd = Database.UserManager.GetPasswordByUserId(currentUserId);
            return pswd;
        }

        public string GetUserNameByUserID(string currentUserId)
        {
            var userName = Database.UserManager.GetUserNameByUserID(currentUserId);
            return userName;
        }

    }
}
