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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CarRepairShopRP.Pages.ReplacedParts
{
    [Authorize(Roles = "Mechanic,Admin")]
    public class EditModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;

        public EditModel(CarRepairShopRP.Data.RepairShopContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
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

            ReplacedPart = await _context.ReplacedPart
                .Include(rp => rp.OldPartImage)
                .Include(rp => rp.NewPartBill)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ReplacedPartID == id);

            if (ReplacedPart == null)
            {
                return NotFound();
            }

            if (!RepairNotClosed(ReplacedPart.RepairID))
                return NotFound();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var partToUpdate = await _context.ReplacedPart
                .Include(rp => rp.OldPartImage)
                .Include(rp => rp.NewPartBill)
                .FirstOrDefaultAsync(rp => rp.ReplacedPartID == id.Value);

            if (partToUpdate == null)
                return NotFound();

            _context.Entry(partToUpdate)
               .Property("RowVersion").OriginalValue = ReplacedPart.RowVersion;

            if (await TryUpdateModelAsync<ReplacedPart>(partToUpdate, "ReplacedPart",
                p => p.Name, p => p.Manufacturer, p => p.ProductionDate, p => p.Quantity, p => p.Price))
            {


              

                string wwwRoot = _hostEnviroment.WebRootPath;
                if (ReplacedPart.OldPartImage.File != null)
                {


              
                    string extension = Path.GetExtension(ReplacedPart.OldPartImage.File.FileName);
                    ReplacedPart.OldPartImage.FileName = Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMdd_HH_mm_ss") + extension;
                    string path = Path.Combine(wwwRoot + "/imgs/oldparts/", ReplacedPart.OldPartImage.FileName);

                    if (partToUpdate.OldPartImage != null)
                    {
                        var imagePath = Path.Combine(wwwRoot, "imgs/oldparts", partToUpdate.OldPartImage.FileName);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                            partToUpdate.OldPartImage.FileName = ReplacedPart.OldPartImage.FileName;
                            partToUpdate.OldPartImage.Title = ReplacedPart.OldPartImage.Title;
                        }
                    }
                    else
                    {
                        var image = new FileModel
                        {
                            Title = ReplacedPart.OldPartImage.Title,
                            FileName = ReplacedPart.OldPartImage.FileName
                        };
                        _context.Files.Add(image);
                        partToUpdate.OldPartImage = image;
                    }


                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await ReplacedPart.OldPartImage.File.CopyToAsync(fileStream);
                    }

                }

                if (ReplacedPart.NewPartBill != null)
                {

                    string extension = Path.GetExtension(ReplacedPart.NewPartBill.File.FileName);
                    ReplacedPart.NewPartBill.FileName = Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMdd_HH_mm_ss") + extension;
                    string path = Path.Combine(wwwRoot + "/bills/", ReplacedPart.NewPartBill.FileName);

                    if (partToUpdate.NewPartBill != null)
                    {
                        var imagePath = Path.Combine(wwwRoot, "bills", partToUpdate.NewPartBill.FileName);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                            partToUpdate.NewPartBill.FileName = ReplacedPart.NewPartBill.FileName;
                            partToUpdate.NewPartBill.Title = ReplacedPart.NewPartBill.Title;
                        }
                    }
                    else
                    {
                        var billModel = new FileModel
                        {
                            Title = ReplacedPart.NewPartBill.Title,
                            FileName = ReplacedPart.NewPartBill.FileName
                        };
                        _context.Files.Add(billModel);
                        partToUpdate.NewPartBill = billModel;
                    }


                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await ReplacedPart.NewPartBill.File.CopyToAsync(fileStream);
                    }

                }


                try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                    var exceptionEntry = ex.Entries.Single();
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save. " +
                            "The replaced part was deleted by another user.");
                        return Page();
                    }

                    var dbValues = (ReplacedPart)databaseEntry.ToObject();
                    ModelState.AddModelError(string.Empty,
                     "The Replaced Part you attempted to edit "
                   + "was modified by another user after you. The "
                   + "edit operation was canceled and the current values in the database "
                   + "have been displayed. If you still want to edit this part, click "
                   + "the Save button again.");


                    ReplacedPart.RowVersion = (byte[])dbValues.RowVersion;
                    // Clear the model error for the next postback.
                    ModelState.Remove("ReplacedPart.RowVersion");

                    return Page();
                }
               
            }
            return RedirectToPage("/Repairs/Index", "id", new { id = partToUpdate.RepairID });
        }
            private bool ReplacedPartExists(int id)
            {
                return _context.ReplacedPart.Any(e => e.ReplacedPartID == id);
            }

         private bool RepairNotClosed(int id)
         {
             return _context.Repair.Any(r => r.RepairID == id && r.InvoiceIssued == false);
         }
    }

}
