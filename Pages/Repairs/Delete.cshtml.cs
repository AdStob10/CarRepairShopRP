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

namespace CarRepairShopRP.Pages.Repairs
{
    [Authorize(Roles = "Mechanic,Admin")]
    public class DeleteModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userMananger;

        public DeleteModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userMananger)
        {
            _context = context;
            _userMananger = userMananger;
        }

        [BindProperty]
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
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.RepairID == id);

            if (Repair == null)
            {
                return NotFound();
            }

            if(Repair.InvoiceIssued)
            {
                return RedirectToPage("./Index");
            }

            var user = await _userMananger.GetUserAsync(User);

            if (!user.Equals(Repair.Client) && !User.IsInRole("Mechanic"))
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Repair = await _context.Repair.FindAsync(id);

            if (Repair != null)
            {
                _context.Repair.Remove(Repair);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
