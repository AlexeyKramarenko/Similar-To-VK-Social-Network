using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
   public class Profile
    {
        [Key]
        public int ProfileID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        //Главная страница
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
         




        //public string AvatarUrl { get; set; }
        //Основное
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public int BirthDay { get; set; }
        public int BirthMonth { get; set; }
        public int BirthYear { get; set; }
        public string HomeTown { get; set; }
        public string Wife { get; set; }
        public string Husband { get; set; }     
        public string Language { get; set; } 





        // Контакты
        public string Country { get; set; }
        public string Town { get; set; }



        //public string PhoneNumber { get; set; }




        public string Skype { get; set; }
        public string WebSite { get; set; }

        //Интересы
        public string Activity { get; set; }
        public string Interests { get; set; }
        public string Music { get; set; }
        public string Films { get; set; }
        public string Books { get; set; }
        public string Games { get; set; }
        public string Quotes { get; set; }
        public string AboutMe { get; set; }

        //Образовнаие
        public string SchoolCountry { get; set; }
        public string SchoolTown { get; set; }
        public string School { get; set; }
        public int StartSchoolYear { get; set; }
        public int FinishSchoolYear { get; set; }
         
    }
}
