using BorjaSwimSchoolApp.DbContext;
using BorjaSwimSchoolApp.Models;
using BorjaSwimSchoolApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorjaSwimSchoolApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email
                };
                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Visitor");
                    await _context.SaveChangesAsync();
                    //await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager
                    .PasswordSignInAsync(vm.Email, vm.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(vm.Email);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Administrator"))
                    {
                        return RedirectToAction("Index", "Administrator");
                    }
                    if (roles.Contains("Coach"))
                    {
                        return RedirectToAction("Index", "Entrenador");
                    }
                    if (roles.Contains("Swimmer"))
                    {
                        return RedirectToAction("Index", "Alumno");
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Login Failure");
            }
            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AllUser()
        {
            var users = await _context.Users.ToListAsync();
            var roles = await _context.Roles.ToListAsync();
            var usersInRoles = await _context.UserRoles.ToListAsync();

            List<AccountAllUserViewModel> listaUsersInRoles = new List<AccountAllUserViewModel>();
            foreach (var item in usersInRoles)
            {
                AccountAllUserViewModel vm = new AccountAllUserViewModel();

                var currentUser = await _userManager.FindByIdAsync(item.UserId);
                var currentRole = await _roleManager.FindByIdAsync(item.RoleId);

                vm.User = currentUser;
                vm.Role = currentRole;
                listaUsersInRoles.Add(vm);
            }
            return View(listaUsersInRoles.OrderBy(x => x.Role.Name).ThenBy(x => x.User.UserName).ToList());
        }
    }
}
