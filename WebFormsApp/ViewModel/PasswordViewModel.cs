using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFormsApp.ViewModel
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage = "Поле \"Пароль\" должно быть заполнено")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" должно быть заполнено")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)] //asp.net identity needs 6 characters
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)] //asp.net identity needs 6 characters
        public string NewPasswordConfirm { get; set; }

    }
}
