using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class SelectListItem
    {
        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="SelectListItem"/> is disabled.
        /// </summary>
       
            
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        

    }
}
