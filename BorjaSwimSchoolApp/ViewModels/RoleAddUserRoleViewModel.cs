using BorjaSwimSchoolApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BorjaSwimSchoolApp.ViewModels
{
    public class RoleAddUserRoleViewModel
    {
        public ApplicationUser User { get; set; }

        public string Role { get; set; }

        public SelectList RoleList { get; set; }
    }
}
