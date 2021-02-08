using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Authorization;

namespace CarRepairShopRP.Pages.Repairs
{
    [Authorize(Roles = "Client,Mechanic")]
    public class IndexModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
       

        public IndexModel(CarRepairShopRP.Data.RepairShopContext context)
        {
            _context = context;
    
        }

        public IList<Repair> Repair { get; set; }


        public int RepairID { get; set; }
        public RepairIndexData RepairData { get; set; }



        //public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }


        public async Task OnGetAsync(int? id, string sortOrder, int? pageIndex, string searchString,  string currentFilter, string searchUser)
        {
            CurrentSort = sortOrder;
            RepairData = new RepairIndexData();

            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            //NameSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Repair> repairsIQ;

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (User.IsInRole("Client"))
            {
                repairsIQ = from r in _context.Repair
                            .Include(r => r.Client)
                            .Where(r => r.Client.UserName == User.Identity.Name)
                            .Include(r => r.AssignedMechanic)
                            .Include(r => r.ReplacedParts)
                            select r;




                //RepairData.Repairs = await _context.Repair
                //    .Include(r => r.Client)
                //    .Where(r => r.Client.UserName == User.Identity.Name)
                //    .Include(r => r.AssignedMechanic)
                //    .Include(r => r.ReplacedParts)
                //    .AsNoTracking()
                //    .OrderByDescending(r => r.startTime)
                //    .ToListAsync();



                //Repair = await _context.Repair
                //    .Include(r => r.Client)
                //    .Where( r => r.Client.UserName == User.Identity.Name)
                //    .Include( r => r.AssignedMechanic)
                //    .AsNoTracking()
                //    .ToListAsync();
            }
            else
            {
                repairsIQ = from r in _context.Repair
                           .Include(r => r.Client)
                           //.Where(r => r.Client.UserName == User.Identity.Name)
                           .Include(r => r.AssignedMechanic)
                           .Include(r => r.ReplacedParts)
                            select r;

                if (!String.IsNullOrEmpty(searchString))
                {
                    repairsIQ = repairsIQ.Where(s => s.Client.FirstName.Contains(searchString)
                                           || s.Client.LastName.Contains(searchString));
                }

                if (!String.IsNullOrEmpty(searchUser))
                {
                    repairsIQ = repairsIQ.Where(s => s.AssignedMechanic.UserName == searchUser);
                }
            }

            switch (sortOrder)
            {

                case "date_desc":
                    repairsIQ = repairsIQ.OrderByDescending(s => s.startTime);
                    break;
                default:
                    repairsIQ = repairsIQ.OrderBy(s => s.startTime);
                    break;
            }

            int pageSize = 5;
            RepairData.Repairs = await PaginatedList<Repair>.CreateAsync(
                repairsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);


            if ( id != null)
            {
                RepairID = id.Value;
                Repair rep = RepairData.Repairs.Single(r => r.RepairID == id.Value);
                RepairData.ReplacedParts = rep.ReplacedParts;
                RepairData.blockNewParts = rep.InvoiceIssued;
            }
        }
    }
}
