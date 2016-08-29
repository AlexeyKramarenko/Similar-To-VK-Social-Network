
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  Core.POCO
{
    public class Agreement
    {
        [Key]
        public int AgreementID { get; set; }
        [Required]
        public string Terms { get; set; }
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Timestamp { get; set; }
         
        public List<ApplicationUser> Accounts { get; set; }
    }
}
