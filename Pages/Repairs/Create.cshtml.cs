using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CarRepairShopRP.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace CarRepairShopRP.Pages.Repairs
{

    [Authorize(Roles = "Mechanic,Admin")]
    public class CreateModel : RepairPageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly UserManager<RepairShopUser> _userManager;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(CarRepairShopRP.Data.RepairShopContext context,   UserManager<RepairShopUser> userManager, ILogger<CreateModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }


        public IActionResult OnGet()
        {
            initializeSelectList(_context);
            return Page();
        }

        [BindProperty]
        public Repair Repair { get; set; }

        [BindProperty]
        public Car Car { get; set; }

       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var emptyCar = new Car();

            if (await TryUpdateModelAsync<Car>(emptyCar, "Car",
                s => s.Brand, s => s.Model, s => s.productionYear, s => s.EngineCapacity, s => s.EngineFuel, s => s.oilChangeDate, s => s.BodyType))
            {
                _context.Car.Add(emptyCar);
            }
            else
                return Page();

            var emptyRepair = new Repair();
            
            if(await TryUpdateModelAsync<Repair>(emptyRepair,"Repair",
                s => s.Description , s => s.startTime , s => s.WorkPrice, s => s.ClientID, s => s.ProblemDescription, s => s.RepairState, s => s.ChangeOil))
            {
                emptyRepair.Car = emptyCar;
                ClaimsPrincipal currentUser = this.User;
                emptyRepair.AssignedMechanic = await _userManager.GetUserAsync(currentUser);
                _context.Repair.Add(emptyRepair);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            initializeSelectList(_context);
            return Page();
        }
    }
}
