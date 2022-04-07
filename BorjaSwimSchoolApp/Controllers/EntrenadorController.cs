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
    [Authorize(Roles = "Coach")]
    public class EntrenadorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public EntrenadorController(UserManager<ApplicationUser> userManager,
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

            Entrenador entrenador = await _context.Entrenadores.Where(e => e.User.Id == currentUserId).FirstOrDefaultAsync();
            return View(entrenador);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(Entrenador entrenador)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var coach = await _context.Entrenadores
                .FirstOrDefaultAsync(e => e.EntrenadorId == entrenador.EntrenadorId);

            if (coach != null)
            {
                //Update
                coach.Nombre = entrenador.Nombre;
                coach.Apellido = entrenador.Apellido;
                coach.Telefono = entrenador.Telefono;

                _context.Entry(coach).State = EntityState.Modified;
            }
            else
            {
                //Create
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = await _userManager.FindByIdAsync(currentUserId);
                entrenador.User = user;

                _context.Entrenadores.Add(entrenador);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Eliminar(int id)
        //{
        //    var entrenadorToDelete = await _context.Entrenadores
        //        .FirstOrDefaultAsync(e => e.EntrenadorId == id);

        //    _context.Remove(entrenadorToDelete);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}
    }
}
