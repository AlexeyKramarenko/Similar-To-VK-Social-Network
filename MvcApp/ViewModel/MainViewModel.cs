
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcApp.ViewModel
{
    public class MainViewModel
    {
        public int ProfileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool Married { get; set; }
        public int BirthDay { get; set; }
        public int BirthMonth { get; set; }
        public int BirthYear { get; set; }
        public string HomeTown { get; set; }
        public string Language { get; set; }

        public List<SelectListItem> BirthYears { get; set; }
        public List<SelectListItem> BirthMonths { get; set; }
        public List<SelectListItem> BirthDays { get; set; }
        public List<SelectListItem> MaritalStatuses { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        public List<SelectListItem> Languages { get; set; }
        
    }
}
