using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CarRepairShopRP.Areas.Identity.Data;

namespace CarRepairShopRP.Pages.ReplacedParts
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;
        public DetailsModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ReplacedPart ReplacedPart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            if (User.IsInRole("Client"))
            {
                var user = await _userManager.GetUserAsync(User);

                ReplacedPart = (from rp in _context.ReplacedPart.Include(r => r.OldPartImage).Include(rp => rp.NewPartBill)
                                join r in _context.Repair on rp.RepairID equals r.RepairID
                                where r.RepairID == id.Value && r.ClientID == user.Id
                                select rp).FirstOrDefault();
                return NotFound();
            }
            else
            {
                ReplacedPart = await _context.ReplacedPart.Include(rp => rp.OldPartImage).Include(rp => rp.NewPartBill).FirstOrDefaultAsync(m => m.ReplacedPartID == id);

            }


            if (ReplacedPart == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
