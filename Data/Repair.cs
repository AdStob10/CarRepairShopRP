using CarRepairShopRP.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Data
{

    public enum RepairState
    {
        Reported, Proceeding, Finished
    }

    public class Repair
    {

        [Display(Name = "Repair Number")]
        public int RepairID { get; set; }

        [Required]
        [StringLength(150,MinimumLength = 10)]
        [Display(Name = "Repair Informations")]
        public string Description { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 10)]
        [Display(Name = "Problem Description")]
        public string ProblemDescription { get; set; }


        [Display(Name = "State of Repair")]
        [DisplayFormat(NullDisplayText = "No state")]
        public RepairState? RepairState { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Start of Repair")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime startTime { get; set; }



        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "Price of Repair")]
        public decimal WorkPrice { get; set; }

        [Display(Name = "Need to change oil")]
        public bool ChangeOil { get; set; }


        public decimal SumPrice { get {

                if (ReplacedParts == null) return WorkPrice;
                decimal sum = 0;
                foreach (var item in ReplacedParts)
                    sum += item.Price;
                sum += WorkPrice;
                    return sum;
        } }

        public ICollection<ReplacedPart> ReplacedParts { get; set; }


        public bool InvoiceIssued { get; set; }
        public Invoice Invoice;


        public int CarID { get; set; }
        public Car Car { get; set; }

        public string ClientID { get; set; }
        public RepairShopUser Client { get; set; }


        public string AssignedMechanicID { get; set; }
        [Display(Name = "Assigned Mechanic")]
        public RepairShopUser AssignedMechanic { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
