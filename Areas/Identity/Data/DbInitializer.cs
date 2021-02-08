using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Areas.Identity.Data
{
   


        public static class DbInitializer
        {
            public async static Task Initialize(RepairShopContext context, UserManager<RepairShopUser> userManager, RoleManager<ApplicationRole> roleManager)
            {
                context.Database.EnsureCreated();

                string[] roleNames = { "Admin", "Client", "Mechanic" };
                Task<IdentityResult> roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = roleManager.RoleExistsAsync(roleName);
                    roleExist.Wait();
                    if (!roleExist.Result)
                    {
                        ApplicationRole ap = new ApplicationRole();
                        ap.Name = roleName;
                        //create the roles and seed them to the database: Question 1
                        roleResult = roleManager.CreateAsync(ap);
                        roleResult.Wait();
                    }
                }
            // Look for any students.
            if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                var client = new RepairShopUser { UserName = "client@example.pl", Email = "client@example.pl", FirstName = "John", LastName = "Kowalsky" };
                await userManager.CreateAsync(client, "Test!@#123");
                 client.EmailConfirmed = true;
                await userManager.AddToRoleAsync(client, "Client");



               var mechanic  = new RepairShopUser { UserName = "mechanic@example.pl", Email = "mechanic@example.pl", FirstName = "Mark", LastName = "Stafford" };
                await userManager.CreateAsync(mechanic, "Test!@#123");
                 mechanic.EmailConfirmed = true;
                await userManager.AddToRoleAsync(mechanic, "Mechanic");


                var admin = new RepairShopUser { UserName = "admin@example.pl", Email = "admin@example.pl", FirstName = "Admin", LastName = "Admin" };
                await userManager.CreateAsync(admin, "Test!@#123");
                admin.EmailConfirmed = true;
                 await userManager.AddToRoleAsync(admin, "Admin");



                var car = new Car
                {
                    Brand = "Audi",
                    Model = "A4",
                    BodyType = BodyType.Sedan,
                    EngineFuel = EngineFuel.Diesel,
                    EngineCapacity = 1900,
                    productionYear = 1999
                };

                context.Car.Add(car);


                 var repair = new Repair
                 {
                     Description="This a example repair description created for testing purposes. Real description will be much longer and more interesting",
                     ProblemDescription = "This a example of problem description created for testing purposes. Real description will be much longer and more interesting",
                     RepairState = RepairState.Reported,
                     startTime = DateTime.Now,
                     WorkPrice = 1,
                     Client = client,
                     AssignedMechanic = mechanic,
                     Car = car,
                 };


                context.Repair.Add(repair);

                var image = new FileModel
                {
                    FileName = "lamp.jpg",
                    Title = "Old lamp"
                };

                context.Files.Add(image);

                var replacedPart = new ReplacedPart
                {
                    Manufacturer = "Audi",
                    Name = "Front lamp",
                    Price = 100,
                    ProductionDate = DateTime.Now,
                    Quantity = 1,
                    Repair = repair,
                    OldPartImage= image

                };

                context.ReplacedPart.Add(replacedPart);



                var visit = new Visit
                  {
                      VisitClient = client,
                      VisitMechanic = mechanic,
                      VisitPurpose = "This a example visit description created for testing purposes. Real description will be much longer and more interesting",
                      AcceptedClient = true,
                      AcceptedMechanic = false,
                      PlannedVisitDate = DateTime.Now
                  };

                context.Visit.Add(visit);
                context.SaveChanges();
            }

 

        internal static void Initialize(RepairShopContext context)
        {
            throw new NotImplementedException();
        }
    }
    
}
