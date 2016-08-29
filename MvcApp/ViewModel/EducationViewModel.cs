 
using System.Collections.Generic; 
using System.Web.Mvc;

namespace MvcApp.ViewModel
{
    public class EducationViewModel
    {
        public int ProfileID { get; set; }
      
        public string SchoolCountry { get; set; }
        public string SchoolTown { get; set; }
        public string School { get; set; }
        public string StartSchoolYear { get; set; }
        public string FinishSchoolYear { get; set; }

        public List<SelectListItem> SchoolCountries { get; set; }
        public List<SelectListItem> SchoolTowns { get; set; }
        public List<SelectListItem> StartYears { get; set; }
        public List<SelectListItem> FinishYears { get; set; }

    }
}
