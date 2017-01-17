using Microsoft.AspNet.Identity;
using Core.BLL.Interfaces;
using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebFormsApp.Services;

namespace WebFormsApp.WebApi
{
    public class SettingsController : ApiController
    {

        ISettingsService settingsService;
        IProfileService profileService;
        ISessionService sessionService;
        public string CurrentUserId
        {
            get
            {
                return sessionService.CurrentUserId;
            }
        }
        public SettingsController(ISettingsService settingsService, IProfileService profileService, ISessionService sessionService)
        {
            this.settingsService = settingsService;
            this.profileService = profileService;
            this.sessionService = sessionService;
        }

        [HttpGet]
        [ActionName("GetPrivacyVisibilityLevels")]
        public string[] GetPrivacyVisibilityLevels(int id)
        {
            var visLevels = settingsService.GetVisibilityLevelNameForEachPrivacySection(id);
            return visLevels;
        }

        [HttpPut]
        [ActionName("UpdatePrivacyFlagForChoosenSection")]
        public void UpdatePrivacyFlagForChoosenSection([FromBody]PrivacyFlag pf)
        {
            settingsService.UpdatePrivacyFlagForChoosenSection(pf);
        }

        [HttpGet]
        [ActionName("GetProfileID")]
        public int GetProfileID()
        {
            string userId = sessionService.CurrentUserId;
            return profileService.GetProfile(userId).ProfileID;
        }

    }
}
