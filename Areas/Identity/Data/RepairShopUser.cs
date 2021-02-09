using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CarRepairShopRP.Data;
using Microsoft.AspNetCore.Identity;

namespace CarRepairShopRP.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the RepairShopUser class
    public class RepairShopUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName="nvarchar(100)")]
        [Required]
        [StringLength(100,MinimumLength = 3)]
       public string FirstName { get; set; }
        
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name =  "Full Name")]
        public string FullName
        {

            get
            {
                return FirstName + " " + LastName;
            }

        }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        [InverseProperty("Client")]
        public virtual ICollection<Repair> Repairs { get; set; }

        [InverseProperty("AssignedMechanic")]
        public virtual ICollection<Repair> AssignedRepairs { get; set; }


        [InverseProperty("VisitClient")]
        public virtual ICollection<Visit> Visits { get; set; }

        [InverseProperty("VisitMechanic")]
        public virtual ICollection<Visit> AssignedVisits { get; set; }
    }
}
