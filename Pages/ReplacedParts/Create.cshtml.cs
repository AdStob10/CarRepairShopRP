using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace CarRepairShopRP.Pages.ReplacedParts
{
    [Authorize(Roles = "Mechanic,Admin")]
    public class CreateModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;

        public CreateModel(CarRepairShopRP.Data.RepairShopContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _hostEnviroment = hostEnviroment;
        }

        public IActionResult OnGet(int id)
        {

            if (!RepairNotClosed(id))
                return NotFound();

            RepairID = id;
            return Page();
        }

        [BindProperty]
        public ReplacedPart ReplacedPart { get; set; }

        public int RepairID { get; set; }

       
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyPart = new ReplacedPart();

            if (await TryUpdateModelAsync<ReplacedPart>(emptyPart, "ReplacedPart",
                p => p.Name, p => p.Manufacturer, p => p.ProductionDate, p => p.Quantity, p => p.Price))
            {
                var repair = await _context.Repair.FindAsync(id);
                if (repair == null || repair.InvoiceIssued)
                    return NotFound();
                emptyPart.Repair = repair;

                string wwwRoot = _hostEnviroment.WebRootPath;
              

                if (ReplacedPart.NewPartBill != null)
                {

     
                    string extension = Path.GetExtension(ReplacedPart.NewPartBill.File.FileName);
                    ReplacedPart.NewPartBill.FileName = Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMdd_HH_mm_ss") + extension;
                    string path = Path.Combine(wwwRoot + "/bills/", ReplacedPart.NewPartBill.FileName);

                    var billModel = new FileModel
                    {
                        Title = ReplacedPart.NewPartBill.Title,
                        FileName = ReplacedPart.NewPartBill.FileName
                    };

                    using (var fileSteam = new FileStream(path, FileMode.Create))
                    {
                        await ReplacedPart.NewPartBill.File.CopyToAsync(fileSteam);
                    }

                    _context.Files.Add(billModel);
                    emptyPart.NewPartBill = billModel;
                }

                if (ReplacedPart.OldPartImage.File != null)
                {


                    string extension = Path.GetExtension(ReplacedPart.OldPartImage.File.FileName);
                    ReplacedPart.OldPartImage.FileName = Path.GetRandomFileName() + DateTime.Now.ToString("yyyyMMdd_HH_mm_ss") + extension;
                    string path = Path.Combine(wwwRoot + "/imgs/oldparts/", ReplacedPart.OldPartImage.FileName);

                    var image = new FileModel
                    {
                        Title = ReplacedPart.OldPartImage.Title,
                        FileName = ReplacedPart.OldPartImage.FileName
                    };

                    using (var fileSteam = new FileStream(path, FileMode.Create))
                    {
                        await ReplacedPart.OldPartImage.File.CopyToAsync(fileSteam);
                    }

                    _context.Files.Add(image);
                    emptyPart.OldPartImage = image;
                }

                _context.ReplacedPart.Add(emptyPart);
                await _context.SaveChangesAsync();
            }



            //return RedirectToPage("/Repairs/Index");
            return RedirectToPage("/Repairs/Index", "id", new { id = id });
        }

        private bool RepairNotClosed(int id)
        {
            return _context.Repair.Any(r => r.RepairID == id && r.InvoiceIssued == false);
        }
    }
}
