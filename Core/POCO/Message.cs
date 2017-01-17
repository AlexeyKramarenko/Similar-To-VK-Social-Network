using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public int DialogID { get; set; }
        public string SendersUserID { get; set; }
        public string ReceiversUserID { get; set; }
        public string Body { get; set; }
        public DateTime RequestDate { get; set; }
        public bool ViewedByReceiver { get; set; }
        public bool Invitation { get; set; }

       
    }
}
