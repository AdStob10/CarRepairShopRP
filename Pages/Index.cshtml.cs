using CarRepairShopRP.Areas.Identity.Data;
using CarRepairShopRP.Pages.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;
        private readonly SignInManager<RepairShopUser> _signManager;

        public IndexModel(ILogger<IndexModel> logger, CarRepairShopRP.Data.RepairShopContext context, UserManager<RepairShopUser> userManager, SignInManager<RepairShopUser> signManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
        }

        [BindProperty]
        public HomeViewModel Home { get; set; }

        public async Task OnGetAsync()
        {
            if(_signManager.IsSignedIn(User))
            {
                if(User.IsInRole("Client"))
                {
                    var user = await _userManager.GetUserAsync(User);


                    Home = new HomeViewModel
                    {
                        id = user.Id,
                        name = user.FullName
                    };
                }
                else
                {
                    var user = await _userManager.GetUserAsync(User);


                    Home = new HomeViewModel
                    {
                        id = user.Id,
                        name = user.FullName,
                        role = "Mechanic"
                    };

                    Home.visits = await _context.Visit
                                        .Where(v => v.VisitMechanicID == Home.id)
                                        .CountAsync();

                    Home.repairs = await _context.Repair
                                        .Where(v => v.AssignedMechanicID == Home.id)
                                        .CountAsync();
                    
                }
            }
        }
    }
}
