 
using System.ComponentModel.DataAnnotations;
 

namespace WebFormsApp.ViewModel
{
    public class ContactsViewModel  
    {
        public int ProfileID { get; set; }   

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered mobile format is not valid.")]
        public string PhoneNumber { get; set; }
        public string Skype { get; set; }
        public string WebSite { get; set; }
    }
}
