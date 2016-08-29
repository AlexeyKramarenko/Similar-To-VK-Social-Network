using Microsoft.AspNet.Identity;
using Core.BLL.Interfaces;
using Core.POCO;
using System.Collections.Generic;
using System.Web.Http;
using Ninject;

namespace MvcApp.WebAPI
{
    public class ProfileController : ApiController
    {
        string currentUserId;

        public IProfileService profileService { get; set; }

        public ProfileController(IProfileService _profileService)
        {
            currentUserId = User.Identity.GetUserId();
            profileService = _profileService;
        }

        [HttpGet]
        [ActionName("GetProfileBirthDays")]
        public string[] GetProfileBirthDays()
        {
            return profileService.GetBirthDays();
        }

        [HttpGet]
        [ActionName("GetProfileBirthMonths")]
        public string[] GetProfileBirthMonths()
        {
            return profileService.GetBirthMonths();
        }
        [HttpGet]
        [ActionName("GetProfileSchoolStartYears")]
        public string[] GetProfileSchoolStartYears()
        {
            return profileService.GetSchoolStartYears(currentUserId);
        }


        [HttpGet]
        [ActionName("GetProfileSchoolFinishYears")]
        public string[] GetProfileSchoolFinishYears()
        {
            return profileService.GetSchoolFinishYears(currentUserId);
        }

        [HttpGet]
        [ActionName("GetProfileBirthYears")]
        public string[] GetProfileBirthYears()
        {
            return profileService.GetBirthYears(currentUserId);
        }

        [HttpGet]
        [ActionName("GetEducationInfo")]
        public List<string> GetEducationInfo()
        {
            return profileService.GetEducationInfo(currentUserId);
        }
        [HttpPut]
        [ActionName("UpdateUserBirthDay")]
        public void UpdateUserBirthDay([FromBody] Profile profile)
        {
            profileService.UpdateBirthDay(profile);
        }
        [HttpPut]
        [ActionName("UpdateUserBirthMonth")]
        public void UpdateUserBirthMonth([FromBody] Profile profile)
        {
            profileService.UpdateBirthMonth(profile);
        }
        [HttpPut]
        [ActionName("UpdateUserBirthYear")]
        public void UpdateUserBirthYear([FromBody] Profile profile)
        {
            profileService.UpdateBirthYear(profile);
        }
        ////[HttpPost]
        ////[ActionName("UpdateProfileEducationData")]
        ////public void UpdateProfileEducationData([FromBody]Profile profile)
        ////{
        ////    profileService.SaveEducation(profile, currentUserId);
        ////}
        //[HttpPost]
        //[ActionName("UpdateProfileEducationData")]
        //public void UpdateProfileEducationData([FromBody]profile profile)
        //{
        //    //profileService.SaveEducation(profile, currentUserId);
        //}

        //public class profile
        //{
        //    public int ProfileID { get; set; }
        //    public string SchoolCountry { get; set; }
        //    public string SchoolTown { get; set; }
        //    public string School { get; set; }
        //    public int StartScoolYear { get; set; }
        //    public int FinishScoolYear { get; set; }
        //}
    }
}
