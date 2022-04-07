using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BorjaSwimSchoolApp.Models
{
    public class Sesion
    {
        [Key]
        public int SesionId { get; set; }

        [Required(ErrorMessage = "Fecha de inicio es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de inicio")]
        public DateTime FxInicio { get; set; }

        [Required(ErrorMessage = "Fecha de finalización es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de finalización")]
        public DateTime FxFin { get; set; }

        [Required(ErrorMessage = "Hora de inicio es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Hora de inicio")]
        public DateTime HoraInicio { get; set; }

        [Required(ErrorMessage = "Número de plazas es requerido")]
        [Display(Name = "Número de plazas")]
        public int Capacidad { get; set; }

        [Range(1, 1000000, ErrorMessage = "Debe especificar un Curso para esta Sesión")]
        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }
    }
}
