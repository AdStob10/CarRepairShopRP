using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CarRepairShopRP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CarRepairShopRP.Pages.Visits
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;


        public EditModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public SelectList mechanicSL { get; set; }

        [BindProperty]
        public Visit Visit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if(User.IsInRole("Client"))
            {
                var user = await _userManager.GetUserAsync(User);
                Visit = await _context.Visit.Where(v => v.VisitClientID == user.Id).FirstOrDefaultAsync(m => m.ID == id);
            }
            else
            {
                Visit = await _context.Visit.FirstOrDefaultAsync(m => m.ID == id);
            }

            if (Visit == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Client"))
            {
                var mechanicQuery = from c in _context.Users
                                    join ur in _context.UserRoles on c.Id equals ur.UserId
                                    join r in _context.Roles on ur.RoleId equals r.Id
                                    where r.Name == "Mechanic"
                                    orderby c.FirstName, c.LastName
                                    select c;
                mechanicSL = new SelectList(mechanicQuery, "Id", "FullName", Visit.VisitMechanic);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int ? id)
        {
            if (id == null)
                return NotFound();
            
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var visitToUpdate = await _context.Visit.FindAsync(id);

            if (visitToUpdate == null)
                return NotFound();

            var acceptedDate = visitToUpdate.PlannedVisitDate;




            if (await TryUpdateModelAsync<Visit>(visitToUpdate,"Visit",
                s => s.PlannedVisitDate, s => s.AcceptedClient , s => s.AcceptedMechanic, s => s.VisitMechanicID))
            {
                if (DateTime.Compare(acceptedDate, visitToUpdate.PlannedVisitDate) != 0)
                {
                    if (User.IsInRole("Mechanic"))
                        visitToUpdate.AcceptedClient = false;
                    else
                        visitToUpdate.AcceptedMechanic = false;
                }


                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(Visit.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

          
            }


            return RedirectToPage("./Index");
        }

        private bool VisitExists(int id)
        {
            return _context.Visit.Any(e => e.ID == id);
        }
    }
}
