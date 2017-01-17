using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BLL.DTO
{
    public class MessageDTO
    {
        public string InterlocutorAvatar { get; set; }
        public string InterlocutorUserName { get; set; }
        public string InterlocutorUserID { get; set; }
        public DateTime CreateDate { get; set; }
        public string MessageText { get; set; }
        public int DialogID { get; set; }      
    }
}
