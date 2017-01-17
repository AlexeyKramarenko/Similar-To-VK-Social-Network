using Core.BLL.DTO;
using Core.BLL.Interfaces;
using Core.POCO;
using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsApp.Services;
using WebFormsApp.ViewModel;

namespace WebFormsApp
{
    public partial class Profile : System.Web.UI.Page
    {
        [Inject]
        public IMappingService MappingService { get; set; }
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


        #region MainInfo

        public MainViewModel GetMainInfo()
        {
            var profile = ProfileService.GetMainInfo(CurrentUserId);

            MainViewModel mainInfo = MappingService.Map<Core.POCO.Profile, MainViewModel>(profile);
            mainInfo.BirthDays = ListItemsFromStringArray(ProfileService.GetBirthDays());
            mainInfo.BirthMonths = ListItemsFromStringArray(ProfileService.GetBirthMonths());
            mainInfo.BirthYears = ListItemsFromStringArray(ProfileService.GetBirthYears(CurrentUserId));

            mainInfo.MaritalStatuses = new ListItem[] { new ListItem { Text = "Single", Value = "Single" }, new ListItem { Text = "Married", Value = "Married" } };
            mainInfo.Languages = new ListItem[] { new ListItem { Text = "Ukrainian", Value = "Ukrainian" }, new ListItem { Text = "English", Value = "English" } };
            mainInfo.GenderList = new ListItem[] { new ListItem { Text = "Male", Value = "Male" }, new ListItem { Text = "Female", Value = "Female" } };
            return mainInfo;
        }

        public void SaveMainInfo(MainViewModel mainInfo)
        {
            string message;

            if (ModelState.IsValid)
            {
                try
                {
                    var profile = MappingService.Map<MainViewModel, Core.POCO.Profile>(mainInfo);
                    ProfileService.SaveMainInfo(profile, CurrentUserId);
                    message = "Main info updated succesfully";
                }
                catch
                {
                    message = "Error";
                }
            }
            else
            {
                message = "Some errors in your form: " + GetAllModelErrors();
            }

            ResultMessage("main_info", message);
        }

        #endregion

        #region Contacts
        public ContactsViewModel GetContacts()
        {
            var profile = ProfileService.GetContacts(CurrentUserId);

            var contacts = MappingService.Map<Core.POCO.Profile, ContactsViewModel>(profile);
            contacts.PhoneNumber = UserService.GetPhoneNumber(CurrentUserId);

            return contacts;
        }
        public void SaveContacts(ContactsViewModel contactsvm)
        {
            string message;

            if (ModelState.IsValid)
            {
                try
                {
                    var profile = MappingService.Map<ContactsViewModel, Core.POCO.Profile>(contactsvm);
                    ProfileService.SaveContacts(profile);
                    UserService.UpdatePhoneNumber(CurrentUserId, contactsvm.PhoneNumber);
                    message = "Contact info updated succesfully";
                }
                catch
                {
                    message = "Error";
                }
            }
            else
            {
                message = "Some errors in your form: " + GetAllModelErrors();
            }

            ResultMessage("contact_info", message);
        }
        #endregion

        #region Interests
        public InterestsViewModel GetInterests()
        {
            var profile = ProfileService.GetInterests(CurrentUserId);
            var interests = MappingService.Map<Core.POCO.Profile, InterestsViewModel>(profile);
            return interests;
        }

        public void SaveInterests(InterestsViewModel model)
        {
            string message;

            if (ModelState.IsValid)
            {
                try
                {
                    var profile = MappingService.Map<InterestsViewModel, Core.POCO.Profile>(model);
                    ProfileService.SaveInterests(profile, CurrentUserId);
                    message = "Interests info updated succesfully";
                }
                catch
                {
                    message = "Error";
                }
            }
            else
            {
                message = "Some errors in your form: " + GetAllModelErrors();
            }

            ResultMessage("interests_info", message);
        }
        #endregion

        #region Education

        private int graduationYear = 0;
        public EducationViewModel GetEducationInfo()
        {

            EducationDTO dto = ProfileService.GetEducationInfoOfUser(CurrentUserId);
            EducationViewModel model = MappingService.Map<EducationDTO, EducationViewModel>(dto);
            
            model.SchoolCountries = ListItemsFromStringArray(dto.CountriesList);
            model.SchoolTowns = ListItemsFromStringArray(dto.Towns.Select(a => a.TownName));
            model.StartYears = ListItemsFromStringArray(dto.StartYears);
            model.FinishYears = ListItemsFromStringArray(dto.FinishYears);
            graduationYear = model.FinishSchoolYear;

            return model;
        }

        public void UpdateEducationInfo(EducationViewModel vm, [Form]string SchoolTown, [Form]int FinishSchoolYear)
        {
            string message;

            if (ModelState.IsValid)
            {
                try
                {
                    vm.SchoolTown = SchoolTown;
                    vm.FinishSchoolYear = FinishSchoolYear;
                    var profile = MappingService.Map<EducationViewModel, Core.POCO.Profile>(vm);
                    ProfileService.SaveEducation(profile, CurrentUserId);
                    message = "Education info updated succesfully";
                }
                catch
                {
                    message = "Error";
                }
            }
            else
            {
                message = "Some errors in your form: " + GetAllModelErrors();
            }

            ResultMessage("education_info", message);
        }

        #endregion 



        private ListItem[] ListItemsFromStringArray(IEnumerable<string> str)
        {
            ListItem[] items = new ListItem[str.Count()];

            for (int i = 0; i < str.Count(); i++)
            {
                items[i] = new ListItem { Text = str.ElementAt(i), Value = str.ElementAt(i) };
            }

            return items;
        }

        private string GetAllModelErrors()
        {
            string message = "";

            for (int i = 0; i < ModelState.Values.Count; i++)
            {
                ModelErrorCollection col = ModelState.Values.ElementAt(i).Errors;

                foreach (var msg in col)
                    message += msg.ErrorMessage;
            }

            return message;
        }

        private void ResultMessage(string key, string message)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), key, "alert('" + message + "');", true);
        }

        private void FillEducationInfoDropDownLists()
        {
            string script = @"vm.getProfileSchoolTown();
                              vm.updateFinishYears("+ graduationYear + ")";

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "dropdownlists", script, true);
        }

        public void Page_PreRenderComplete(object s, EventArgs e)
        {
       //     if (IsPostBack)
                FillEducationInfoDropDownLists();
        }
    }
}