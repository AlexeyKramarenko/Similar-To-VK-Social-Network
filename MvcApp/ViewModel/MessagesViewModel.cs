using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApp.ViewModel
{
    public class MessagesViewModel
    {
        public string InterlocutorAvatar { get; set; }
        public string InterlocutorUserName { get; set; }
        public string InterlocutorUserID { get; set; }
        public string CurrentUserAvatar { get; set; }
        public string CurrentUserName { get; set; }
        public string CreateDate { get; set; }
        public string MessageText { get; set; }
        public int DialogID { get; set; }

       
    }
}
