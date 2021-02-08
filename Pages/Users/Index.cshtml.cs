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
    public class IndexModel : PageModel
    {

        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;
        public IndexModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
       
        }
        public IList<RepairShopUser> Users { get; set; }
 

        public async Task OnGetAsync()
        {

            Users = await _context.Users.Include(r => r.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
        }
    }
}
