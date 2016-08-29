using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class Relationship
    {
        [Key]
        public int FriendID { get; set; }
        public string SenderAccountID { get; set; }
        public string ReceiverAccountID { get; set; }


        [ForeignKey("RelationshipDefinition")]
        public int RelationshipDefinitionID { get; set; }
        public RelationshipDefinition RelationshipDefinition { get; set; }

    }
    

}
