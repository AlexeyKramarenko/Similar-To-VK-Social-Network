using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class Friend
    {
        [Key]
        public int ID { get; set; }

        public string AccountID { get; set; }
        public string FriendID { get; set; }
    }
}
