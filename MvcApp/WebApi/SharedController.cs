using System.Web.Http;
using Core.BLL.Interfaces;
using MvcApp.Services;

namespace MvcApp.WebAPI
{
    public class SharedController : ApiController
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
        public SharedController(IProfileService profileService, ISessionService sessionService)
        {
            this.profileService = profileService;
            this.sessionService = sessionService;
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

        [HttpGet]
        [ActionName("GetProfileSchoolTown")]
        public IHttpActionResult GetProfileSchoolTown()
        {
            var town = profileService.GetProfileSchoolTown(CurrentUserId);

            if (town != null)
                return Ok(town);

            else
                return NotFound();
        }

    }
}
