using BorjaSwimSchoolApp.DbContext;
using BorjaSwimSchoolApp.Models;
using BorjaSwimSchoolApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BorjaSwimSchoolApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdministratorController(UserManager<ApplicationUser> userManager,
                                       ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllCurso()
        {
            var cursos = await _context.Cursos.Include(c => c.Entrenador).ToListAsync();
            return View(cursos);
        }

        [HttpGet]
        public async Task<IActionResult> AddCurso()
        {
            var entrenadorDisplay = await _context.Entrenadores.Select(x => new
            {
                Id = x.EntrenadorId,
                Value = x.Nombre + " " + x.Apellido
            }).OrderBy(x => x.Value).ToListAsync();

            AdministratorAddCursoViewModel vm = new AdministratorAddCursoViewModel();
            vm.EntrenadorList = new Microsoft.AspNetCore.Mvc.Rendering
                .SelectList(entrenadorDisplay, "Id", "Value");

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddCurso(AdministratorAddCursoViewModel vm)
        {
            var entrenador = await _context.Entrenadores
                .SingleOrDefaultAsync(e => e.EntrenadorId == vm.Entrenador.EntrenadorId);
            vm.Curso.Entrenador = entrenador;

            _context.Cursos.Add(vm.Curso);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Administrator");
        }
    }
}
