using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.POCO
{
    public class Status
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public string WallOfUserID { get; set; }
        public string PostByUserID { get; set; }
        public string Post { get; set; }

        [NotMapped]
        public string AvatarUrl { get; set; }
        public int CommentsCount { get; set; }
        public DateTime CreateDate { get; set; }
        public ApplicationUser User { get; set; }

        [NotMapped]
        public string UserName { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }


    public class InsertStatusResult
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
    }
}
