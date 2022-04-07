using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BorjaSwimSchoolApp.Models
{
    public class Entrenador
    {
        [Key]
        public int EntrenadorId { get; set; }

        [Required(ErrorMessage = "Nombre de entrenador es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido de entrenador es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Teléfono es requerido")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        public virtual ICollection<Curso> Cursos { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
