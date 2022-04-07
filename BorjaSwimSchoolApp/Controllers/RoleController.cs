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
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RoleController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult AllRole()
        {
            var roles = _roleManager.Roles.OrderBy(x => x.Name).ToList();

            return View(roles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("AllRole");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddUserRole(string id)
        {
            var roleDisplay = await _context.Roles.Select(x => new
            {
                x.Id,
                Value = x.Name
            }).ToListAsync();

            RoleAddUserRoleViewModel vm = new RoleAddUserRoleViewModel();

            var user = await _userManager.FindByIdAsync(id);
            vm.User = user;
            vm.RoleList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(roleDisplay, "Id", "Value");

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserRole(RoleAddUserRoleViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.User.Id);
            var role = await _roleManager.FindByIdAsync(vm.Role);
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction("AllUser", "Account");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            var roleDisplay = _context.Roles.Select(x => new
            {
                x.Id,
                Value = x.Name
            }).ToList();

            vm.User = user;
            vm.RoleList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(roleDisplay, "Id", "Value");

            return View(vm);
        }

        public async Task<IActionResult> DeleteUserRole(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.RemoveFromRoleAsync(user, role);

            if (result.Succeeded)
            {
                return RedirectToAction("AllUser", "Account");
            }
            return View();
        }
    }
}
