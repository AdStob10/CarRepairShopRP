using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRepairShopRP.Data;

namespace CarRepairShopRP.Pages.Invoices
{
    public class testModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;

        public testModel(CarRepairShopRP.Data.RepairShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RepairID"] = new SelectList(_context.Repair, "RepairID", "Description");
            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Invoice.Add(Invoice);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
