using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.BLL.DTO
{
    public class UserDTO
    {
        public string UserID { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserDTO() { }

        public UserDTO(string id, string imageUrl, string username, string firstname, string lastname)
        {
            UserID = id;
            ImageUrl = imageUrl;
            UserName = username;
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
