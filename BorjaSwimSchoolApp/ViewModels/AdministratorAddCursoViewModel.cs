using Microsoft.AspNetCore.Mvc.Rendering;
using BorjaSwimSchoolApp.Models;

namespace BorjaSwimSchoolApp.ViewModels
{
    public class AdministratorAddCursoViewModel
    {
        public Curso Curso { get; set; }

        public Entrenador Entrenador { get; set; }

        public SelectList EntrenadorList { get; set; }
    }
}
