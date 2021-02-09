using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRepairShopRP.Data;
using CarRepairShopRP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CarRepairShopRP.Pages.Repairs
{
    public class DetailsModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userMananger;

        public DetailsModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager)
        {
            _context = context;
            _userMananger = userManager;
        }

        public Repair Repair { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Repair = await _context.Repair
                .Include(r => r.Client)
                .Include(r => r.AssignedMechanic)
                .Include(r => r.Car)
                .Include(r => r.ReplacedParts)
                .FirstOrDefaultAsync(m => m.RepairID == id);

            if (Repair == null)
            {
                return NotFound();
            }

            var user = await _userMananger.GetUserAsync(User);

            if (!user.Equals(Repair.Client) && User.IsInRole("Client"))
            {
                return NotFound();
            }


            return Page();
        }
    }
}
