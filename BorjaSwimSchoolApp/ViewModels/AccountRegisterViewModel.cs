using System.ComponentModel.DataAnnotations;

namespace BorjaSwimSchoolApp.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required(ErrorMessage = "Dirección de E-mail es requerido"), MaxLength(256)]
        [EmailAddress(ErrorMessage = "La Dirección de E-mail no es válida")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Dirección de E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido"), MinLength(8), MaxLength(36)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmar contraseña es requerido"), MinLength(8), MaxLength(36)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña no coincide")]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
