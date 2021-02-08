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
using Microsoft.AspNetCore.Authorization;

namespace CarRepairShopRP.Pages.Visits
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;

        public IndexModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public string DateSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
       // public IList<Visit> Visit { get; set; }


        public PaginatedList<Visit> Visit {get;set;}

        public async Task OnGetAsync(string sortOrder, int ? pageIndex, string searchString, string currentFilter, string searchUser)
        {
            IQueryable<Visit> visitsIQ;
            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

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
                var user = await _userManager.GetUserAsync(User);
                visitsIQ = from v in _context.Visit
                         .Include(v => v.VisitClient)
                         .Include(v => v.VisitMechanic)
                         .Where(v => v.VisitClient == user)
                         select v;
            }
            else
            {
                visitsIQ = from v in _context.Visit
                         .Include(v => v.VisitClient)
                         .Include(v => v.VisitMechanic)
                         select v;

                if (!String.IsNullOrEmpty(searchString))
                {
                    visitsIQ = visitsIQ.Where(s => s.VisitClient.FirstName.Contains(searchString)
                                           || s.VisitClient.LastName.Contains(searchString));
                }

                if (!String.IsNullOrEmpty(searchUser))
                {
                    visitsIQ = visitsIQ.Where(s => s.VisitMechanic.UserName == searchUser);
                }

            }


            switch (sortOrder)
            {

                case "Date":
                    visitsIQ = visitsIQ.OrderBy(v => v.PlannedVisitDate);
                    break;
                case "date_desc":
                    visitsIQ = visitsIQ.OrderByDescending(v => v.PlannedVisitDate);
                    break;
            }

            int pageSize = 7;
            Visit = await PaginatedList<Visit>.CreateAsync(
                visitsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
