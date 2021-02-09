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
using Microsoft.Extensions.Logging;

namespace CarRepairShopRP.Pages.Repairs
{
    [Authorize(Roles ="Mechanic,Admin")]
    public class EditModel : RepairPageModel
    {
        private readonly CarRepairShopRP.Data.RepairShopContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(CarRepairShopRP.Data.RepairShopContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Repair Repair { get; set; }
        


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Repair = await _context.Repair
                .Include(r => r.Car)
                .Include(r => r.Client )
                .Include(r => r.AssignedMechanic)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RepairID == id);


            if (Repair == null)
            {
                return NotFound();
            }

            initializeSelectList(_context, Repair.Client, Repair.AssignedMechanic);

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


            var repairToUpdate = await _context.Repair.FindAsync(id);

            if (repairToUpdate == null)
                return NotFound();

            if (await TryUpdateModelAsync<Repair>(repairToUpdate, "Repair",
                 s => s.Description, s => s.startTime, s => s.WorkPrice, s => s.ClientID, s => s.ProblemDescription, s => s.RepairState, s => s.ChangeOil))
            {

                _logger.LogInformation(repairToUpdate.CarID.ToString());
                var carToUpdate = await _context.Car.FindAsync(repairToUpdate.CarID);

                if (await TryUpdateModelAsync<Car>(carToUpdate, "Repair.Car",
                    s => s.Brand, s => s.Model, s => s.productionYear, s => s.EngineCapacity, s => s.EngineFuel, s => s.oilChangeDate, s => s.BodyType))
                {
                    /*
                    carToUpdate.Brand = Repair.Car.Brand;
                    carToUpdate.Model = Repair.Car.Model;
                    carToUpdate.productionYear = Repair.Car.productionYear;
                    carToUpdate.EngineCapacity = Repair.Car.EngineCapacity;
                    carToUpdate.EngineFuel = Repair.Car.EngineFuel;
                    carToUpdate.oilChangeDate = Repair.Car.oilChangeDate;
                    carToUpdate.BodyType = Repair.Car.BodyType;
                        */
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RepairExists(Repair.RepairID))
                        {
                            return NotFound();
                        }


                        if (!CarExists(Repair.Car.CarID))
                        {
                            return NotFound();
                        }
                        throw;



                    }
                }
                

               
            }

            return RedirectToPage("./Index");
        }

        private bool RepairExists(int id)
        {
            return _context.Repair.Any(e => e.RepairID == id);
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.CarID == id);
        }
    }
}
