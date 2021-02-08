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

namespace CarRepairShopRP.Pages.ReplacedParts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userMananger;

        public IndexModel(CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userMananger)
        {
            _context = context;
            _userMananger = userMananger;
        }

        public IList<ReplacedPart> ReplacedPart { get;set; }
        public int RepairID { get; set; }

        public async Task OnGetAsync(int id)
        {
            RepairID = id;
            if (User.IsInRole("Client"))
            {
                var user = await _userMananger.GetUserAsync(User);

                ReplacedPart = await _context.ReplacedPart.Include(r => r.Repair).Where(p => p.RepairID == id && p.Repair.ClientID == user.Id).AsNoTracking().ToListAsync();
            }
            else
            {
                ReplacedPart = await _context.ReplacedPart.Include(r => r.Repair).Where(p => p.RepairID == id ).AsNoTracking().ToListAsync();
            }

        }
    }
}
