using System.ComponentModel.DataAnnotations;

namespace BorjaSwimSchoolApp.Models
{
    public class Matricula
    {
        [Key]
        public int MatriculaId { get; set; }

        [Range(1, 1000000, ErrorMessage = "Debe especificar un Alumno para esta Matrícula")]
        public int AlumnoId { get; set; }
        public virtual Alumno Alumno { get; set; }

        //[Range(1, 1000000, ErrorMessage = "Debe especificar un Curso para esta Matrícula")]
        //public int CursoId { get; set; }
        //public virtual Curso Curso { get; set; }

        [Range(1, 1000000, ErrorMessage = "Debe especificar una Sesión para esta Matrícula")]
        public int SesionId { get; set; }
        public virtual Sesion Sesion { get; set; }
    }
}
