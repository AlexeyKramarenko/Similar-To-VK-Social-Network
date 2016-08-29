 
using System.Collections.Generic;
 
namespace  Core.POCO
{

    public class VisibilityLevel
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        public ICollection<PrivacyFlag> PrivacyFlags { get; set; }


    }

}

