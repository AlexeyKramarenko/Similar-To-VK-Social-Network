 
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
        public int StartSchoolYear { get; set; }
        public int FinishSchoolYear { get; set; }

        public SelectListItem[] SchoolCountries { get; set; }
        public SelectListItem[] SchoolTowns { get; set; }
        public SelectListItem[] StartYears { get; set; }
        public SelectListItem[] FinishYears { get; set; }

    }
}
