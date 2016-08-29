using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFormsApp.ViewModel
{
    public class UserViewModel
    {
        public string UserID { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserViewModel() { }

        public UserViewModel(string id, string imageUrl, string username,string firstname, string lastname)
        {
            UserID = id;
            ImageUrl = imageUrl;
            UserName = username;
            FirstName = firstname;
            LastName = lastname;
        }
    }
}

