using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Identity;
using CarRepairShopRP.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CarRepairShopRP.Pages.Visits
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;

        public CreateModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Visit Visit { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Visit emptyVisit = new Visit();

            if(await TryUpdateModelAsync<Visit>(emptyVisit,"Visit",
                v => v.VisitPurpose, v => v.PlannedVisitDate
             ))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound();

                emptyVisit.VisitClient = user;
                emptyVisit.AcceptedClient = true;
                _context.Visit.Add(emptyVisit);
                await _context.SaveChangesAsync();

            }

            return RedirectToPage("./Index");
        }
    }
}
