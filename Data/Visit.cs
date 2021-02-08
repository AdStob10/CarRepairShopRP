using CarRepairShopRP.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Data
{
    public class Visit
    {
        [Display(Name = "Visit Number")]
        public int ID { get; set; }

        
        [Display(Name = "Visit Purpose")]
        [StringLength(140, MinimumLength = 15, ErrorMessage = "Visit purpose must have from {2} to {1} characters")]
        public string VisitPurpose { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Visit date")]
        public DateTime PlannedVisitDate { get; set; }

        [Display(Name = "Accepted by Client")]
        [Required]
        public bool AcceptedClient { get; set; }

        [Display(Name = "Accepted by Mechanic")]
        [Required]
        public bool AcceptedMechanic { get; set; }


        public string VisitMechanicID { get; set; }
        [Display(Name = "Mechanic")]
        public RepairShopUser VisitMechanic { get; set; }

        public string VisitClientID { get; set; }
        [Display(Name = "Client")]
        public RepairShopUser VisitClient { get; set; }
    }
}
