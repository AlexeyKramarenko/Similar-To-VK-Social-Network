using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BLL.DTO
{
  public  class MessageDTO
    {
        public string InterlocutorAvatar { get; set; }
        public string InterlocutorUserName { get; set; }
        public string InterlocutorUserID { get; set; }
        public string CurrentUserAvatar { get; set; }
        public string CurrentUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string MessageText { get; set; }
        public int DialogID { get; set; }

        public MessageDTO()
        {

        }
        public MessageDTO(string InterlocutorAvatar, string InterlocutorUserName, string InterlocutorUserID, string CurrentUserAvatar, string CurrentUserName, DateTime CreateDate, string MessageText, int DialogID)
        {
            this.InterlocutorAvatar = InterlocutorAvatar;
            this.InterlocutorUserName = InterlocutorUserName;
            this.InterlocutorUserID = InterlocutorUserID;
            this.CurrentUserAvatar = CurrentUserAvatar;
            this.CurrentUserName = CurrentUserName;
            this.CreateDate = CreateDate;
            this.MessageText = MessageText;
            this.DialogID = DialogID;
        }
    }
}
