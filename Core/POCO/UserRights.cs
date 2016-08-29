using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.POCO
{
    public class UserRights
    {
        [Key]
        public int UserRightID { get; set; }
        
        public int RelationshipDefinitionID { get; set; }
        
        public int VisibilityLevelId { get; set; }

     
    }
}
