using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BorjaSwimSchoolApp.Models
{
    public enum Nivel
    {
        Junior, Glider, Senior, Pro
    }
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "Debe especificar un nivel")]
        public Nivel Nivel { get; set; }

        public virtual ICollection<Sesion> Sesiones { get; set; }

        [Range(1, 1000000, ErrorMessage = "Debe especificar un Entrenador para este Curso")]
        public int EntrenadorId { get; set; }
        public virtual Entrenador Entrenador { get; set; }
    }
}
