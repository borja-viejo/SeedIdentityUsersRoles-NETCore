using BorjaSwimSchoolApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BorjaSwimSchoolApp.ViewModels
{
    public class AccountAllUserViewModel
    {
        public ApplicationUser User { get; set; }

        public IdentityRole Role { get; set; }
    }
}
