using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class Comment
    {
        public int ID { get; set; }
        public Status Status { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("Status")]
        public int StatusID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

  
        public string UserName { get; set; }
        public string CommentText { get; set; }
        public DateTime CreateDate { get; set; }
        public string WallByUserID { get; set; }
    }
}
