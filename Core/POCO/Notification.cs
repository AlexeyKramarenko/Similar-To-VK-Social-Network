using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }
        public string FromUserID { get; set; }
        public string ToUserID { get; set; } 
    }
}
