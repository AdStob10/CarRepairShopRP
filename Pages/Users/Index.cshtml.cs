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
        public PaginatedList<RepairShopUser> Users { get; set; }

        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string searchString, int? pageIndex, string currentFilter)
        {
            CurrentFilter = searchString;

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            /*
                Users = await _context.Users
                    .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .AsNoTracking()
                    .ToListAsync();
            */

            IQueryable<RepairShopUser> usersIQ = from u in _context.Users.Include(r => r.UserRoles).ThenInclude(ur => ur.Role)
                                               select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                usersIQ = usersIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            int pageSize = 10;
            Users = await PaginatedList<RepairShopUser>.CreateAsync(
                usersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
