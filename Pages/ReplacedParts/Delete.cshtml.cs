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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CarRepairShopRP.Pages.ReplacedParts
{
    [Authorize(Roles = "Mechanic,Admin")]
    public class DeleteModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;
        private readonly IWebHostEnvironment _hostEnviroment;

        public DeleteModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _userManager = userManager;
            _hostEnviroment = hostEnviroment;
        }

        [BindProperty]
        public ReplacedPart ReplacedPart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

                ReplacedPart = await _context.ReplacedPart.Include(rp => rp.OldPartImage).Include(rp => rp.NewPartBill).FirstOrDefaultAsync(m => m.ReplacedPartID == id);

     

            if (ReplacedPart == null)
            {
                return NotFound();
            }

            if (!RepairNotClosed(ReplacedPart.RepairID))
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReplacedPart = await _context.ReplacedPart.Include(rp => rp.OldPartImage).Include(rp => rp.NewPartBill).Where(rp => rp.ReplacedPartID == id.Value).FirstOrDefaultAsync();

            if (ReplacedPart != null)
            {

                if(ReplacedPart.OldPartImage != null)
                {
                    var imagePath = Path.Combine(_hostEnviroment.WebRootPath, "imgs/oldparts", ReplacedPart.OldPartImage.FileName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                        _context.Files.Remove(ReplacedPart.OldPartImage);
                    }

                }

                if (ReplacedPart.NewPartBill != null)
                {
                    var billPath = Path.Combine(_hostEnviroment.WebRootPath, "bills", ReplacedPart.NewPartBill.FileName);
                    if (System.IO.File.Exists(billPath))
                    {
                        System.IO.File.Delete(billPath);
                        _context.Files.Remove(ReplacedPart.NewPartBill);
                    }
                }

                _context.ReplacedPart.Remove(ReplacedPart);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Repairs/Index", "id", new { id = ReplacedPart.RepairID });
        }
        private bool RepairNotClosed(int id)
        {
            return _context.Repair.Any(r => r.RepairID == id && r.InvoiceIssued == false);
        }
    }
}
