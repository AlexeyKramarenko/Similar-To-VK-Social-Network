using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace  Core.POCO
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            AgreementID = 1;            
        }

        public string Password { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public byte[] Timestamp { get; set; }
        public DateTime? AgreedToTermsDate { get; set; }
        public int StatusesCount { get; set; }

        [NotMapped]
        public Profile Profile { get; set; }

        [ForeignKey("Agreement")]
        public int AgreementID { get; set; }

        public Agreement Agreement { get; set; }
        public List<Relationship> Friends { get; set; }
    }
}
