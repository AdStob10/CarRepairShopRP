using CarRepairShopRP.Areas.Identity.Data;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Pages.Repairs
{
    public class RepairPageModel:PageModel
    {

        public SelectList clientSL { get; set; }
        public SelectList mechanicSL { get; set; }
        //public SelectList repairState { get; set; }
        //public SelectList engineFuels { get; set; }

        //public SelectList bodyTypes { get; set; }


        public  void initializeSelectList( RepairShopContext _context, object selectedClient = null, object selectedMechanic = null)
        {
            //var clientQuery = await _userManager.GetUsersInRoleAsync("Client");


            var clientQuery = from c in _context.Users
                              join ur in _context.UserRoles on c.Id equals ur.UserId
                              join r in _context.Roles on ur.RoleId equals r.Id
                              where r.Name ==  "Client"
                              orderby c.FirstName, c.LastName
                              select c;

            var mechanicQuery = from c in _context.Users
                              join ur in _context.UserRoles on c.Id equals ur.UserId
                              join r in _context.Roles on ur.RoleId equals r.Id
                              where r.Name == "Mechanic"
                              orderby c.FirstName, c.LastName
                              select c;

            clientSL = new SelectList(clientQuery, "Id", "FullName", selectedClient);
            mechanicSL = new SelectList(mechanicQuery, "Id", "FullName", selectedMechanic);

            //repairState = new SelectList(Enum.GetValues(typeof(RepairState)));
            //engineFuels = new SelectList(Enum.GetValues(typeof(EngineFuel)), selectedEF);
            //bodyTypes = new SelectList(Enum.GetValues(typeof(BodyType)), selectedBT);


        }

    }
}
