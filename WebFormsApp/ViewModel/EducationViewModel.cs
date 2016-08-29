using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFormsApp.ViewModel
{
    public class EducationViewModel
    {
        public int ProfileID { get; set; }
        //Образовнаие
        public string SchoolCountry { get; set; }
        public string SchoolTown { get; set; }
        public string School { get; set; }
        public string StartScoolYear { get; set; }
        public string FinishScoolYear { get; set; }
    }
}
