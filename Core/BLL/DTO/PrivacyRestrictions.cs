using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BLL.DTO 
{
    public class PrivacyRestrictions
    {
        public bool DisplayDetailsInfo = true;
        public bool DisplayPosts = true;
        public bool DisplayMessageForm = true;
        public bool DisplayComments = true;
        public bool DisplayCommentLink = true;
        public bool SendMessagePossibility = true;
        public bool ShowInSearch = true;
    }
}
