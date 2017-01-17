

using System.Web.UI.WebControls;

namespace WebFormsApp.ViewModel
{
    public class MainViewModel
    {
        public int ProfileID { get; set; }        
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; } 
        public string BirthDay { get; set; }
        public string BirthMonth { get; set; }
        public string BirthYear { get; set; }
        public string HomeTown { get; set; }
        public string Language { get; set; }

        public ListItem[] BirthYears { get; set; }
        public ListItem[] BirthMonths { get; set; }
        public ListItem[] BirthDays { get; set; }
        public ListItem[] MaritalStatuses { get; set; }
        public ListItem[] GenderList { get; set; }
        public ListItem[] Languages { get; set; }
    }
}
