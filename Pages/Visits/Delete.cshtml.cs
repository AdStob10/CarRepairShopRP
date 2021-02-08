﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Authorization;
using CarRepairShopRP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;


namespace CarRepairShopRP.Pages.Visits
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;

        public DeleteModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Visit Visit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Client"))
            {
                var user = await _userManager.GetUserAsync(User);

                Visit = await _context.Visit
                               .Include(v => v.VisitMechanic)
                               .Include(v => v.VisitClient)
                               .Where(v => v.VisitClientID == user.Id)
                               .FirstOrDefaultAsync(m => m.ID == id);
            }
            else
            {
                Visit = await _context.Visit
                           .Include(v => v.AcceptedMechanic)
                           .Include(v => v.AcceptedClient)
                            .FirstOrDefaultAsync(m => m.ID == id);
            }

            if (Visit == null)
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

            Visit = await _context.Visit.FindAsync(id);

            if (Visit != null)
            {
                _context.Visit.Remove(Visit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
