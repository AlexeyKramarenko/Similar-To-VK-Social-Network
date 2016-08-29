using Core.BLL.Interfaces;
using System.Web.Http; 

namespace WebFormsApp.WebApi
{
    public class SharedController : ApiController
    { 
        IProfileService profileService;

        public SharedController(IProfileService _profileService)
        {
            profileService = _profileService;
        }

        [HttpGet]
        [ActionName("GetAgeYears")]
        public IHttpActionResult GetAgeYears()
        {
            var years = profileService.GetAgeYears();

            if (years != null)
                return Ok(years);

            else
                return NotFound();
        }

        [HttpGet]
        [ActionName("GetCountries")]
        public IHttpActionResult GetCountries()
        {
            var countries = profileService.GetCountries();

            if (countries != null)
                return Ok(countries);

            else
                return NotFound();
        }

        [HttpGet]
        [ActionName("GetProfileTowns")]
        public IHttpActionResult GetProfileTowns(string id)
        {
            var towns = profileService.GetProfileTowns(id);

            if (towns != null)
                return Ok(towns);

            else
                return NotFound();
        }

    }
}
