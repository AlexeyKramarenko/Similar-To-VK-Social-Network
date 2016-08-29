 
using System.ComponentModel.DataAnnotations;
 

namespace WebFormsApp.ViewModel
{
    public class ContactsViewModel  
    {
        public int ProfileID { get; set; }      
        public string Country { get; set; }
        public string Town { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Skype { get; set; }
        public string WebSite { get; set; }
    }
}
