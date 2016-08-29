
using Microsoft.AspNet.Identity;
using Ninject;
using WebFormsApp.ViewModel;
using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI;
using Core.BLL.Interfaces;
using Core.POCO;
using System.Collections.Generic;

namespace WebFormsApp
{
    public partial class _Profile : BasePage
    {
        [Inject]
        public IMappingService MappingService { get; set; }

        [Inject]
        public IProfileService ProfileService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }


        private string currentUserId;


        void Page_Init(object s, EventArgs e)
        {
            currentUserId = User.Identity.GetUserId();
        } 


        public void SaveContacts(ContactsViewModel contactsvm)
        {            
            if (ModelState.IsValid)
            { 
                var profile = MappingService.Map<ContactsViewModel, Profile>(contactsvm);

                ProfileService.SaveContacts(profile);

                UserService.UpdatePhoneNumber(currentUserId, contactsvm.PhoneNumber);
            }

        }
        public void SaveInterests(InterestsViewModel interests)
        {
            if (ModelState.IsValid)
            {
                var profile = MappingService.Map<InterestsViewModel, Profile>(interests);
                ProfileService.SaveInterests(profile, currentUserId); 
            } 
        }


        public ContactsViewModel GetContacts()
        {
            var profile = ProfileService.GetContacts(currentUserId);

            var contacts = MappingService.Map<Profile, ContactsViewModel>(profile);
            contacts.PhoneNumber = UserService.GetPhoneNumber(User.Identity.GetUserId());

            return contacts;
        }
        public MainViewModel GetMainInfo()
        {
            var profile = ProfileService.GetMainInfo(currentUserId);
            var mainInfo = MappingService.Map<Profile, MainViewModel>(profile);
            return mainInfo;
        }
        public InterestsViewModel GetInterests()
        {
            var profile = ProfileService.GetInterests(currentUserId);
            var interests = MappingService.Map<Profile, InterestsViewModel>(profile);
            return interests;
        }

        public void SaveMainInfo(
        MainViewModel mainInfo,
        [Control("ddlGender")]string genderValue,
        [Control("ddlMarried")]bool marriedValue,
        [Control("ddlBirthDay")]string birthDayValue,
        [Control("ddlBirthMonth")]string birthMonthValue,
        [Control("ddlBirthYear")]string birthYearValue,
        [Control("ddlLanguage")]string languageValue
        )
        {
            mainInfo.Gender = genderValue;
            mainInfo.Married = marriedValue;
            mainInfo.BirthDay = birthDayValue;
            mainInfo.BirthMonth = birthMonthValue;
            mainInfo.BirthYear = birthYearValue;
            mainInfo.Language = languageValue;

            if (ModelState.IsValid)
            {
                var profile = MappingService.Map<MainViewModel, Profile>(mainInfo);
                ProfileService.SaveMainInfo(profile, currentUserId);

            }
        }
        public List<SelectListItem> GetLanguages()
        {
            return ProfileService.GetLanguages(currentUserId);
        }

        public List<SelectListItem> GetMarried()
        {
            return ProfileService.GetMaritalStatus(currentUserId);
        }

        public List<SelectListItem> GetGender()
        {
            return ProfileService.GetGender(currentUserId);
        }

        public List<SelectListItem> GetBirthDay()
        {
            return ProfileService.GetBirthDay(currentUserId);
        }

        public List<SelectListItem> GetBirthMonth()
        {
            return ProfileService.GetBirthMonth(currentUserId);
        }

        public List<SelectListItem> GetBirthYear()
        {
            return ProfileService.GetBirthYear(currentUserId);
        }


        public List<SelectListItem> GetSchoolTown()
        {
            return ProfileService.GetSchoolTown(currentUserId);
        }

        public List<SelectListItem> GetSchoolStartYear()
        {
            return ProfileService.GetSchoolStartYear(currentUserId);
        }

        public List<SelectListItem> GetSchoolFinishYear()
        {
            return ProfileService.GetSchoolFinishYear(currentUserId);
        }

        public List<SelectListItem> GetSchoolCountry()
        {
            return ProfileService.GetSchoolCountry(currentUserId);
        }


    }
}
