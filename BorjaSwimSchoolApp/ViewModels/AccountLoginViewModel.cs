using System.ComponentModel.DataAnnotations;

namespace BorjaSwimSchoolApp.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required(ErrorMessage = "Dirección de E-mail es requerido"), EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Dirección de E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
