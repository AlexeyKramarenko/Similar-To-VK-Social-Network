using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WebFormsApp.ViewModel
{
    public class EducationViewModel
    {
        public int ProfileID { get; set; }
        //Образовнаие
        public string SchoolCountry { get; set; }
        public string SchoolTown { get; set; }
        public string School { get; set; }
        public int StartSchoolYear { get; set; }
        public int FinishSchoolYear { get; set; }

        public ListItem[] SchoolCountries { get; set; }
        public ListItem[] SchoolTowns { get; set; }
        public ListItem[] StartYears { get; set; }
        public ListItem[] FinishYears { get; set; }
    }
}
