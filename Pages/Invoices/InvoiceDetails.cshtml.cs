using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepairShopRP.Areas.Identity.Data;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRepairShopRP.Pages.Invoices
{
    public class InvoiceDetailsModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;
        private readonly IConfiguration _configuration;


        public InvoiceDetailsModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;

        }


    [BindProperty]
        public InvoiceViewModel InvoiceModel { get; set; }



        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Invoice invoice = await _context.Invoice
                                    .Include(i => i.Repair)
                                        .ThenInclude(r => r.ReplacedParts)
                                     .Include( i => i.IssuedBy)
                                     .Include( i => i.IssuedTo)
                                    .FirstOrDefaultAsync(i => i.RepairID == id.Value);


            if (invoice == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);



            if (!user.Equals(invoice.IssuedTo) && !User.IsInRole("Mechanic"))
            {
                return NotFound();
            }

            //if ( !User.IsInRole("Mechanic"))
            //{
            //    return NotFound();
            //}

            //var seqNum = _context.GetNextDocVal();

            //string newNumber = "FA" + seqNum.ToString("D5") + "/" + DateTime.Now.ToString("yyyy-MM-dd");

            InvoiceModel = new InvoiceViewModel
            {
                DocNum = invoice.InvoiceNumber,
                CompanyName = _configuration["Company:Name"],
                CompanyAddress = _configuration["Company:Address"],
                CompanyPhone = _configuration["Company:Phone"],
                BillUser = invoice.IssuedTo,
                issueDate = invoice.createdDate,
                WorkCost = invoice.Repair.WorkPrice,
                Sum = invoice.Sum,
                IssuedBy = invoice.IssuedBy.FullName,
                Parts = invoice.Repair.ReplacedParts,
                RepairID = invoice.RepairID
            };




            //Invoice = await _context.Invoice
            //    .Include(i => i.Repair).FirstOrDefaultAsync(m => m.InvoiceID == id);


            return Page();
        }

    }
}
