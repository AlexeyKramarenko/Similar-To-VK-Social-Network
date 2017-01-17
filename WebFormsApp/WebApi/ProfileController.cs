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
using WebFormsApp.Services;
using System.Web.Http.Results;

namespace WebFormsApp.WebApi
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
        [HttpPut]
        [ActionName("UpdateUserBirthDay")]
        public void UpdateUserBirthDay([FromBody] Core.POCO.Profile profile)
        {
            profileService.UpdateBirthDay(profile);
        }
        [HttpPut]
        [ActionName("UpdateUserBirthMonth")]
        public void UpdateUserBirthMonth([FromBody] Core.POCO.Profile profile)
        {
            profileService.UpdateBirthMonth(profile);
        }
        [HttpPut]
        [ActionName("UpdateUserBirthYear")]
        public void UpdateUserBirthYear([FromBody] Core.POCO.Profile profile)
        {
            profileService.UpdateBirthYear(profile);
        }
        [HttpPut]
        [ActionName("UpdateProfileEducationData")]
        public void UpdateProfileEducationData([FromBody]Core.POCO.Profile profile)
        {
            profileService.SaveEducation(profile, CurrentUserId);
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
