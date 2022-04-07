using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BorjaSwimSchoolApp.Models
{
    public enum Sexo
    {
        Hombre, Mujer, Otro
    }
    public class Alumno
    {
        [Key]
        public int AlumnoId { get; set; }

        [Required(ErrorMessage = "Nombre de alumno es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido de alumno es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Teléfono es requerido")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Sexo es requerido")]
        public Sexo Sexo { get; set; }

        [Required(ErrorMessage = "Fecha de nacimiento es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FxNacimiento { get; set; }

        public virtual ICollection<Sesion> Sesiones { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
