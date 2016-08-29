using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BLL.DTO
{
    public class FriendsDTO
    {
        public string ImageUrl { get; set; }
        public string FriendsPageUrl { get; set; }
        public FriendsDTO() { }
        public FriendsDTO(string imageUrl, string friendsPageUrl)
        {
            ImageUrl = imageUrl;
            FriendsPageUrl = friendsPageUrl;
        }
    }
}
