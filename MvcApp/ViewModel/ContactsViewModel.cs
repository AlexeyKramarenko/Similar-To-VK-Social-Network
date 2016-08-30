
using System.ComponentModel.DataAnnotations;


namespace MvcApp.ViewModel
{
    public class ContactsViewModel
    {
        public ContactsViewModel()
        {

        }
        public ContactsViewModel(int ProfileID, string Country, string Town, string PhoneNumber, string Skype, string WebSite) 
        {
            this.ProfileID = ProfileID;
            this.Country = Country;
            this.Town = Town;
            this.PhoneNumber = PhoneNumber;
            this.Skype = Skype;
            this.WebSite = WebSite;
        }
        public int ProfileID { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }


        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered mobile format is not valid.")]
        public string PhoneNumber { get; set; }
        public string Skype { get; set; }
        public string WebSite { get; set; }
    }
}
