using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Identity;
using CarRepairShopRP.Areas.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace CarRepairShopRP.Pages.Invoices
{
    [Authorize(Roles = "Mechanic,Admin")]
    public class IssueInvoiceModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IssueInvoiceModel> _logger;


        public IssueInvoiceModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager, IConfiguration configuration, ILogger<IssueInvoiceModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        [BindProperty]
        public InvoiceViewModel InvoiceModel { get; set; }



        public async Task<IActionResult> OnGetAsync(int? id)
        {
      
            if (id == null)
            {
                return NotFound();
            }

            bool exists = await _context.Invoice.AnyAsync(i => i.RepairID == id.Value);

            if (exists)
            {
                return RedirectToPage("/Repairs/Index");
            }
        


            Repair repair = await _context.Repair
                                    .Include(r => r.Client)
                                    .Include(r => r.ReplacedParts)
                                    .FirstOrDefaultAsync(r => r.RepairID == id.Value);

            if (repair == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            decimal sum = 0;
            var result = _context.ReplacedPart.Where(p => p.RepairID == repair.RepairID).GroupBy(p => "1")
                                    .Select(p => p.Sum(i => i.Quantity * i.Price));

            foreach(var grp in result)
            {
                sum += grp;
            }
            //var seqNum = _context.GetNextDocVal();

            //string newNumber = "FA" + seqNum.ToString("D5") + "/" + DateTime.Now.ToString("yyyy-MM-dd");

            InvoiceModel = new InvoiceViewModel {
                CompanyName = _configuration["Company:Name"],
                CompanyAddress = _configuration["Company:Address"],
                CompanyPhone = _configuration["Company:Phone"],
                BillUser = repair.Client,
                issueDate = DateTime.Now,
                WorkCost = repair.WorkPrice,
                Sum = sum + repair.WorkPrice,
                IssuedBy = user.FullName,
                Parts = repair.ReplacedParts,
                RepairID = repair.RepairID
             };




            //Invoice = await _context.Invoice
            //    .Include(i => i.Repair).FirstOrDefaultAsync(m => m.InvoiceID == id);


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
  
            if (!ModelState.IsValid)
            {
                return Page();
            }


            Repair repair = await _context.Repair
                                     .Include(r => r.Client)      
                                     .FirstOrDefaultAsync( r => r.RepairID == InvoiceModel.RepairID);

            if (repair == null)
            {
                return NotFound();
            }

            var u = await _userManager.GetUserAsync(User);

            var seqNum = _context.GetNextDocVal();

            string newNumber = "FA" + seqNum.ToString("D5") + "/" + DateTime.Now.ToString("yyyy-MM-dd");

            _logger.LogInformation("INVOICE TO REPAIR " + repair.RepairID);
            Invoice emptyInvoice = new Invoice
            {
                RepairID = repair.RepairID,
                createdDate = InvoiceModel.issueDate,
                Sum = InvoiceModel.Sum,
                IssuedBy = u,
                InvoiceNumber = newNumber,
                IssuedTo = repair.Client
            };

            _context.Invoice.Add(emptyInvoice);
            repair.InvoiceIssued = true;

            var result = await _context.SaveChangesAsync();

            return RedirectToPage("/Repairs/Index");


        }
        //public async Task<IActionResult> OnPostAync()
        //{

        //    _logger.LogInformation("NEW INVOICE COMING...");
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }


        //    Repair repair = await _context.Repair.FindAsync(InvoiceModel.RepairID);

        //    if(repair == null)
        //    {
        //        return NotFound();
        //    }

        //    var u =  await _userManager.GetUserAsync(User);

        //    var seqNum = _context.GetNextDocVal();

        //    string newNumber = "FA" + seqNum.ToString("D5") + "/" + DateTime.Now.ToString("yyyy-MM-dd");


        //    Invoice emptyInvoice = new Invoice {
        //        Repair = repair,
        //        createdDate = InvoiceModel.issueDate,
        //        Sum = InvoiceModel.Sum,
        //        IssuedBy = u,
        //        InvoiceNumber = newNumber
        //    };

        //    _context.Invoice.Add(emptyInvoice);

        //    var result = await _context.SaveChangesAsync();

        //     return RedirectToPage("/Repairs/Index");

        //}
    }
}
