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
        public string ConcurrencyErrorMessage { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id, bool? concurrencyError)
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

            if (!user.Equals(Repair.Client) && User.IsInRole("Client"))
            {
                return NotFound();
            }


            if (concurrencyError.GetValueOrDefault())
            {
                ConcurrencyErrorMessage = "The Repair you attempted to delete "
                  + "was modified by another user after you selected delete. "
                  + "The delete operation was canceled and the current values in the "
                  + "database have been displayed. If you still want to delete this "
                  + "record, click the Delete button again.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repToDelete = await _context.Repair.Include(r => r.Car).FirstAsync(r => r.RepairID == id);
            _context.Entry(repToDelete)
               .Property("RowVersion").OriginalValue = Repair.RowVersion;
            _context.Entry(repToDelete.Car)
                .Property("RowVersion").OriginalValue = Repair.Car.RowVersion;
            try
            {
                if (repToDelete != null )
                {
         
 
                    if (repToDelete.Car != null)
                    {
                        _context.Car.Remove(repToDelete.Car);
                    }
                    _context.Repair.Remove(repToDelete);
                    await _context.SaveChangesAsync();
                }
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToPage("./Delete",
                    new { concurrencyError = true, id = id });
            }
        }
    }
}
