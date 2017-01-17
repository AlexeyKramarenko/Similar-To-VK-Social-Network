using Microsoft.AspNet.Identity;

using Core.BLL.Interfaces;
using Core.POCO;
using System.Web.Http;
using MvcApp.Services;
using System.Web.Http.Description;

namespace MvcApp.WebAPI
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
        public IHttpActionResult GetPrivacyVisibilityLevels(int id)
        {
            string[] visLevels = settingsService.GetVisibilityLevelNameForEachPrivacySection(id);
            if (visLevels != null)
                return Ok(visLevels);

            return NotFound();
        }
       
        [HttpGet]
        [ActionName("GetProfileID")]
        public int GetProfileID()
        {
            string userId = User.Identity.GetUserId();
            return profileService.GetProfile(userId).ProfileID;
        }

    }
}
