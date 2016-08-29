using Microsoft.AspNet.Identity;
 
using Core.BLL.Interfaces; 
using Core.POCO; 
using System.Web.Http;

namespace MvcApp.WebAPI
{
    public class SettingsController : ApiController
    {

        ISettingsService settingsService;
        IProfileService profileService;
        public SettingsController(ISettingsService _settingsService, IProfileService _profileService)
        {
            settingsService = _settingsService;
            profileService = _profileService;
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
            string userId = User.Identity.GetUserId();
            return profileService.GetProfile(userId).ProfileID;
        }

    }
}
