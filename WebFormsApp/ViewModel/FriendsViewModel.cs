using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFormsApp.ViewModel
{
    public class FriendsViewModel
    {
        public string ImageUrl { get; set; }
        public string FriendsPageUrl { get; set; }
        public FriendsViewModel() { }
        public FriendsViewModel(string imageUrl, string friendsPageUrl)
        {
            ImageUrl = imageUrl;
            FriendsPageUrl = friendsPageUrl;
        }

    }
}
