using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApp.ViewModel
{
    public  class EmailViewModel
    {
        public string OldEmail { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Поле \"Email\" должно быть установлено")]
        public string NewEmail { get; set; }
    }
}
