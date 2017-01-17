using Microsoft.AspNet.Identity;
using Core.BLL.Interfaces;
using Core.POCO;
using System.Collections.Generic;
using System.Web.Http;
using Ninject;
using MvcApp.Services;

namespace MvcApp.WebAPI
{
    public class ProfileController : ApiController
    {
        IProfileService profileService;
        ISessionService sessionService;

        public string CurrentUserId
        {
            get
            {
                return sessionService.CurrentUserId;
            }
        }
        public ProfileController(IProfileService profileService, ISessionService sessionService)
        {
            this.profileService = profileService;
            this.sessionService = sessionService;
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
            return profileService.GetSchoolStartYears(CurrentUserId);
        }


        [HttpGet]
        [ActionName("GetProfileSchoolFinishYears")]
        public string[] GetProfileSchoolFinishYears()
        {
            return profileService.GetSchoolFinishYears(CurrentUserId);
        }

        [HttpGet]
        [ActionName("GetProfileBirthYears")]
        public string[] GetProfileBirthYears()
        {
            return profileService.GetBirthYears(CurrentUserId);
        }

        [HttpGet]
        [ActionName("GetEducationInfo")]
        public List<string> GetEducationInfo()
        {
            return profileService.GetEducationInfo(CurrentUserId);
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
        [HttpGet]
        [ActionName("GetFinishYears")]
        public string[] GetFinishYears(int selectedStartYear)
        {
            string[] finishYears = profileService.GetFinishYears(selectedStartYear, CurrentUserId);

            return finishYears;
        }

    }
}
