using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    
    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        public string CountryName { get; set; }

        public List<Town> Towns { get; set; }
        
    }
    public class Town
    {
        [Key]
        public int TownID { get; set; }
        public string TownName { get; set; }
         
        [ForeignKey("Country")]
        public int CountryID { get; set; }

        public   Country Country { get; set; }
    }
}
