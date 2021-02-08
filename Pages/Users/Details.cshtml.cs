using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepairShopRP.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarRepairShopRP.Pages.Users
{
    [Authorize(Roles = "Mechanic,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly UserManager<RepairShopUser> _userManager;

        public DetailsModel(UserManager<RepairShopUser> userManager)
        {
            _userManager = userManager;
        }

        public RepairShopUser UserToShow { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserToShow = await _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).Where(r => r.Id == id).FirstOrDefaultAsync();
            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
