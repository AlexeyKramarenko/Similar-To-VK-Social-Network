using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    
    public class PrivacyFlagType
    {
         
        public int ID { get; set; }

        public string FieldName { get; set; }

        public ICollection<PrivacyFlag> PrivacyFlags { get; set; }

     
    }
}
