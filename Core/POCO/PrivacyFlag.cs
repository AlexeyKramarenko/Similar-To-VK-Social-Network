using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class PrivacyFlag
    { 
        public int ID { get; set; }
        [ForeignKey("PrivacyFlagType")]
        public int PrivacyFlagTypeID { get; set; }
        [ForeignKey("Profile")]
        public int ProfileID { get; set; }
        [ForeignKey("VisibilityLevel")]
        public int VisibilityLevelID { get; set; }

         
        public PrivacyFlagType PrivacyFlagType { get; set; }
        public Profile Profile { get; set; }
        public VisibilityLevel VisibilityLevel { get; set; }
        
    }
}
