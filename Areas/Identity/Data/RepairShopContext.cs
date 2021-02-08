using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepairShopRP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarRepairShopRP.Data;
using CarRepairShopRP.Pages.Users;
using Microsoft.Data.SqlClient;

namespace CarRepairShopRP.Data
{
    public class RepairShopContext : IdentityDbContext<
        RepairShopUser, ApplicationRole, string,
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public RepairShopContext(DbContextOptions<RepairShopContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            builder.HasSequence<int>("InvoiceNumber")
                    .IsCyclic(true)
                    .StartsAt(0)
                    .HasMax(99999);

            //builder.Entity<Repair>().ToTable("Repairs");


            // builder.Entity<Client>(b =>
            //{
            //    b.HasKey(u => u.Id);

            //    b.Property(u => u.FirstName).HasColumnName("FirstName");
            //    b.Property(u => u.LastName).HasColumnName("LastName");


            //});
        }


        public DbSet<CarRepairShopRP.Data.Repair> Repair { get; set; }
        public DbSet<CarRepairShopRP.Data.Car> Car { get; set; }
        public DbSet<CarRepairShopRP.Data.ReplacedPart> ReplacedPart { get; set; }

        public DbSet<CarRepairShopRP.Data.Invoice> Invoice { get; set; }

        public int GetNextDocVal()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlRaw(
                       "SELECT @result = (NEXT VALUE FOR InvoiceNumber)", result);

            return (int)result.Value;
        }

        public DbSet<CarRepairShopRP.Data.Visit> Visit { get; set; }

        public DbSet<CarRepairShopRP.Data.FileModel> Files { get; set; }


    }
}
