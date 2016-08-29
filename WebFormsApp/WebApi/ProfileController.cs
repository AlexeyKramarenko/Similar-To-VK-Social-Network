using Microsoft.AspNet.Identity; 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Core.POCO;
using Core.BLL.Interfaces;

namespace WebFormsApp.WebApi
{
    public class ProfileController : ApiController
    {        
        string currentUserId;
        IProfileService profileService;
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
        [HttpPut]
        [ActionName("UpdateProfileEducationData")]
        public void UpdateProfileEducationData([FromBody]Profile profile)
        {
            profileService.SaveEducation(profile, currentUserId);
        }



    }
}
