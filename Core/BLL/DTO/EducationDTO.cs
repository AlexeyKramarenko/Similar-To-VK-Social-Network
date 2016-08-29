using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BLL.DTO
{
    public class EducationDTO
    {
        public int ProfileID { get; set; } 
        public string SchoolCountry { get; set; }
        public string SchoolTown { get; set; }
        public string School { get; set; }
        public int StartSchoolYear { get; set; }
        public int FinishSchoolYear { get; set; }


        public string[] StartYears { get; set; }
        public string[] FinishYears { get; set; }
        public SelectListItem[] CountriesList { get; set; }
        public Town[] Towns { get; set; }
    }
}
