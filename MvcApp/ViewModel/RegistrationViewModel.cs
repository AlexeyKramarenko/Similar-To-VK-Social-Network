using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApp.ViewModel
{
    public class RegistrationViewModel
    {
       [Required(ErrorMessage = "Поле \"Имя\" должно быть заполнено")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле \"Фамилия\" должно быть заполнено")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Поле \"Email\" должно быть установлено")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле \"Имя пользователя\" должно быть заполнено")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" должно быть заполнено")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)] //asp.net identity needs 6 characters
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public DateTime BirthDate { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public string MaritalStatus { get; set; }

        public string Gender { get; set; }

    }
}
