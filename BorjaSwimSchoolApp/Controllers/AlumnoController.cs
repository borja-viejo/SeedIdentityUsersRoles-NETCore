using BorjaSwimSchoolApp.DbContext;
using BorjaSwimSchoolApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BorjaSwimSchoolApp.Controllers
{
    [Authorize(Roles = "Swimmer")]
    public class AlumnoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AlumnoController(UserManager<ApplicationUser> userManager,
                                ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Alumno alumno = await _context.Alumnos.Where(x => x.User.Id == currentUserId).FirstOrDefaultAsync();
            return View(alumno);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var swimmer = await _context.Alumnos
                .FirstOrDefaultAsync(e => e.AlumnoId == alumno.AlumnoId);

            if (swimmer != null)
            {
                //Update
                swimmer.Nombre = alumno.Nombre;
                swimmer.Apellido = alumno.Apellido;
                swimmer.Telefono = alumno.Telefono;
                swimmer.Sexo = alumno.Sexo;
                swimmer.FxNacimiento = alumno.FxNacimiento;

                _context.Entry(swimmer).State = EntityState.Modified;
            }
            else
            {
                //Create
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = await _userManager.FindByIdAsync(currentUserId);
                alumno.User = user;

                _context.Alumnos.Add(alumno);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
